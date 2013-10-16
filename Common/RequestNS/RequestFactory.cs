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

    public ARequest CreateWelcomeRequest() {
      return new RequestWelcome(this.RemoteBaseUrl, this.Executor);
    }

    public ARequest CreateGetDrinkRequest() {
      return new RequestDrinkList(this.RemoteBaseUrl, this.Executor);
    }

    public ARequest CreateOrderDrinkRequest(string drinkId) {
      RequestOrderDrink order = new RequestOrderDrink(this.RemoteBaseUrl, this.Executor);
      order.DrinkId = drinkId;
      return order;
    }

  }
}
