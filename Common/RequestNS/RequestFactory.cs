using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestFactory {
    #region Properties
    public IRequestExecutor Executor { get; private set; }
    public string RemoteBaseUrl { get; set; }
    #endregion

    public RequestFactory(IRequestExecutor service) {
      this.Executor = service;
      this.RemoteBaseUrl = string.Empty;
    }

    public RequestWelcome CreateWelcomeRequest() {
      return new RequestWelcome(this.RemoteBaseUrl, this.Executor);
    }

    public RequestDrinkList CreateGetDrinkRequest() {
      return new RequestDrinkList(this.RemoteBaseUrl, this.Executor);
    }

    public RequestOrderDrink CreateOrderDrinkRequest(string drinkId) {
      RequestOrderDrink order = new RequestOrderDrink(this.RemoteBaseUrl, this.Executor);
      order.DrinkId = drinkId;
      return order;
    }

    public RequestOrderStatus CreateOrderStatusRequest(string orderId) {
      RequestOrderStatus order = new RequestOrderStatus(this.RemoteBaseUrl, this.Executor);
      order.OrderId = orderId;
      return order;
    }
  }
}
