using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.RequestNS {
  public class RequestDrinkDetails : ARequest {

    #region Members
    private string _DrinkId = string.Empty;
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
    public RequestDrinkDetails(string baseUrl, IRequestExecutor executor)
      : base(baseUrl, executor) {
      this.RelativeUrl = "/drinks/{0}";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

    #region Public methods
		public DTO.Drink GetDrinkDetails() {
      var rawDrink = JObject.Parse(this.Response);

      DTO.Drink drink = new DTO.Drink();
      drink.DrinkId = rawDrink["id"].ToString();
      drink.Name = rawDrink["name"].ToString();
      drink.Description = rawDrink["description"].ToString();
      drink.Recipe = rawDrink["recipe"].ToString();      

      return drink;
    }
    #endregion

    #region Private methods
    private void _SetDrinkId(string id) {
      this._DrinkId = id;
      this.RelativeUrl = string.Format(this.RelativeUrl, id);
    }
    #endregion
  }
}
