using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestOrderDrink : ARequest {
    #region Members
    private string _DrinkId;
    #endregion

    #region Properties
    public string DrinkId {
      get {
        return this._DrinkId;
      }
      set {
        this._SetDrinkId(value);
      }
    }
    #endregion

    #region Constructor
    public RequestOrderDrink(string baseUrl, IRequestExecutor executor)
      : base(baseUrl, executor) {
      this.RelativeUrl = "/orders/{0}";
      this.ContentType = "application/json";
      this.RequestMethod = "POST";
    }
    #endregion

    #region Public mehtods
    public string GetOrderAmount() {
      return this.Response;
    }
    #endregion

    #region Private methods
    private void _SetDrinkId(string drinkId) {
      this._DrinkId = drinkId;
      this.RelativeUrl = string.Format(this.RelativeUrl, drinkId);
    }
    #endregion
  }
}
