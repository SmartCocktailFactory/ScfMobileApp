using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Model {
  class ScfOrderService : IOrderService {

    #region Members
    private RequestNS.RequestFactory _Factory = null;
    #endregion

    #region Model.IOrderService

    public event EventHandler<OrderChangedEventArgs> OnOrderChanged;



    public ViewModel.Order CurrentOrder {
      get { throw new NotImplementedException(); }
    }


    public void OrderDrink(string drinkId) {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateOrderDrinkRequest(drinkId);
        request.OnRequestCompleted += orderRequest_OnRequestCompleted;
        request.Execute();
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
      order.DrinkName = orderResponse.DrinkId;
      order.OrderId = orderResponse.GetOrderAmount();
      order.ExpectedTimeToDeliver = new TimeSpan(0, 1, 23);

      this._NotifyOrderChanged(order);
    }

    #endregion
  }
}
