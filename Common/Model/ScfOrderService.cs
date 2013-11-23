using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Common.Model {
  public class ScfOrderService : IOrderService {

    #region Members
    private const int UpdateIntervalMs = 10000;
    private RequestNS.RequestFactory _Factory = null;
		private List<DTO.Order> _CurrentOrders = new List<DTO.Order>();
    private object _LockCurrentOrders = new object();
    private Timer _OrderUpdateTick;
    #endregion

    #region Constructor
    public ScfOrderService(RequestNS.RequestFactory requestFactory) {
      this._Factory = requestFactory;

      this._OrderUpdateTick = new Timer(UpdateIntervalMs);
      this._OrderUpdateTick.Elapsed += _OrderUpdateTick_Elapsed;
      this._OrderUpdateTick.Enabled = true;
    }
    #endregion

    #region Model.IOrderService

    public event EventHandler<OrderChangedEventArgs> OnOrderChanged;

    public IList<DTO.Order> CurrentOrders {
      get {
        lock (this._LockCurrentOrders) {
          return new List<DTO.Order>(this._CurrentOrders.Select(x => ((DTO.Order)x.Clone())));
        }
      }
    }

    public void OrderDrink(string drinkId) {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateOrderDrinkRequest(drinkId);
        request.OnRequestCompleted += orderRequest_OnRequestCompleted;
        request.Execute();
      });
    }

    public void UpdateOrderStatus(string orderId) {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateOrderStatusRequest(orderId);
        request.OnRequestCompleted += orderUpdaterequest_OnRequestCompleted;
        request.Execute();
      });
    }
    #endregion

    #region Public methods
    public void ResetService() {
      lock (this._LockCurrentOrders) {
        this._CurrentOrders = new List<DTO.Order>();
      }
    }
    #endregion

    #region Private methods

		private void _NotifyOrderChanged(DTO.Order order) {
      if (this.OnOrderChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnOrderChanged(this, new OrderChangedEventArgs(order));
        });
      }
    }

    private List<string> _GetUncompletedOrderIds() {
      lock (this._LockCurrentOrders) {
        return this._CurrentOrders.Where(x => x.ExpectedSecondsToDeliver > 0).Select(x => x.OrderId).ToList();
      }
    }

    private void _HandleFailedOrderRequest(RequestNS.RequestOrderStatus failedRequest) {
      DTO.Order failedOrder = null;
      lock (this._LockCurrentOrders) {
        failedOrder = this._CurrentOrders.FirstOrDefault(x => x.OrderId == failedRequest.OrderId);
        if (failedOrder != null) {
          this._CurrentOrders.Remove(failedOrder);
        }
      }
      failedOrder.OrderStateId = DTO.StateId.Failed;
      this._NotifyOrderChanged(failedOrder);

      return;
    }

    private void _UpdateOrderList(RequestNS.RequestOrderStatus orderStatus) {
      try {
        lock (this._LockCurrentOrders) {
          DTO.Order editOrder = this._CurrentOrders.FirstOrDefault(x => x.OrderId == orderStatus.OrderId);

          if (editOrder == null) {
            editOrder = new DTO.Order();
            editOrder.OrderId = orderStatus.OrderId;
            this._CurrentOrders.Add(editOrder);
          }

          DTO.Order updatedOrder = orderStatus.GetOrder();

          editOrder.DrinkId = updatedOrder.DrinkId;
          editOrder.ExpectedSecondsToDeliver = updatedOrder.ExpectedSecondsToDeliver;
          editOrder.OrderStatus = updatedOrder.OrderStatus;
          editOrder.OrderStateId = updatedOrder.OrderStateId;

          this._NotifyOrderChanged((DTO.Order)editOrder.Clone());
        }
      } catch (InvalidOperationException) {
      } catch (ArgumentNullException) {
      }
    }
    #endregion

    #region Event handlers

    void orderRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestOrderDrink orderResponse = e.Request as RequestNS.RequestOrderDrink;
      if (e.Request.State != RequestNS.RequestStates.Successful) {
        return;
      }

			DTO.Order order = new DTO.Order();
      order.OrderId = orderResponse.GetOrderAmount();

      lock (this._LockCurrentOrders) {
        this._CurrentOrders.Add(order);
      }
      this.UpdateOrderStatus(order.OrderId);
    }

    void orderUpdaterequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestOrderStatus orderStatus = e.Request as RequestNS.RequestOrderStatus;

      if (e.Request.State != RequestNS.RequestStates.Successful) {
        this._HandleFailedOrderRequest(orderStatus);
      } else {
        this._UpdateOrderList(orderStatus);
      }
    }

    void _OrderUpdateTick_Elapsed(object sender, ElapsedEventArgs e) {
      List<string> uncompletedOrderIds = this._GetUncompletedOrderIds();

      foreach (string orderId in uncompletedOrderIds) {
        this.UpdateOrderStatus(orderId);
      }
    }
    #endregion
  }
}
