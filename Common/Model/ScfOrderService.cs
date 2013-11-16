using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Common.Model {
  class ScfOrderService : IOrderService {

    #region Members
    private const int UpdateIntervalMs = 10000;
    private RequestNS.RequestFactory _Factory = null;
		private List<DTO.Order> _CurrentOrders = new List<DTO.Order>();
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
      get { return this._CurrentOrders; }
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
			this._CurrentOrders = new List<DTO.Order>();
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
      return this.CurrentOrders.Where(x => x.ExpectedSecondsToDeliver > 0).Select(x => x.OrderId).ToList();
    }
    #endregion

    #region Event handlers

    void orderRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestOrderDrink orderResponse = e.Request as RequestNS.RequestOrderDrink;
			DTO.Order order = new DTO.Order();
      order.OrderId = orderResponse.GetOrderAmount();

      this._CurrentOrders.Add(order);
      this.UpdateOrderStatus(order.OrderId);
    }

    void orderUpdaterequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestOrderStatus orderStatus = e.Request as RequestNS.RequestOrderStatus;
      try {
				DTO.Order editOrder = this.CurrentOrders.First(x => x.OrderId == orderStatus.OrderId);

				DTO.Order updatedOrder = orderStatus.GetOrder();

        editOrder.DrinkId = updatedOrder.DrinkId;
        editOrder.ExpectedSecondsToDeliver = updatedOrder.ExpectedSecondsToDeliver;
        editOrder.OrderStatus = updatedOrder.OrderStatus;

        this._NotifyOrderChanged(editOrder);
      } catch (InvalidOperationException) {
      } catch (ArgumentNullException) {
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
