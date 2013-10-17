using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Model {
  class ScfOrderService : IOrderService {

    #region Members
    private RequestNS.RequestFactory _Factory = null;
    private List<ViewModel.Order> _CurrentOrders = new List<ViewModel.Order>();
    #endregion

    #region Constructor
    public ScfOrderService(RequestNS.RequestFactory requestFactory) {
      this._Factory = requestFactory;
    }
    #endregion

    #region Model.IOrderService

    public event EventHandler<OrderChangedEventArgs> OnOrderChanged;

    public IList<ViewModel.Order> CurrentOrders {
      get { return this._CurrentOrders; }
    }

    public void OrderDrink(string drinkId) {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateOrderDrinkRequest(drinkId);
        request.OnRequestCompleted += orderRequest_OnRequestCompleted;
        request.Execute();
      });
    }

    public void UpdateOrderStatus() {
      Task.Factory.StartNew(() => {
        
      });
    }
    #endregion

    #region Private methods

    private void _NotifyOrderChanged(ViewModel.Order order) {
      if (this.OnOrderChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnOrderChanged(this, new OrderChangedEventArgs(order));
        });
      }
    }

    #endregion

    #region Event handlers

    void orderRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestOrderDrink orderResponse = e.Request as RequestNS.RequestOrderDrink;
      ViewModel.Order order = new ViewModel.Order();
      order.OrderId = orderResponse.GetOrderAmount();

      this._CurrentOrders.Add(order);
      this._NotifyOrderChanged(order);
    }

    #endregion
  }
}
