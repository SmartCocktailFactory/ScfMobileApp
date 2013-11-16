using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.RequestNS {
  public class RequestDrinkList : ARequest {

    #region Constructor
    public RequestDrinkList(string baseUrl, IRequestExecutor executor)
      : base(baseUrl, executor) {
      this.RelativeUrl = "/drinks";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

    #region Public methods
		public List<DTO.Drink> GetDrinks() {
			List<DTO.Drink> drinkList = new List<DTO.Drink>();
      var rawDrinks = JObject.Parse(this.Response);

      foreach (var drink in rawDrinks["drinks"]) {
				DTO.Drink d = new DTO.Drink();
        d.DrinkId = drink["id"].ToString();
        d.Name = drink["name"].ToString();
        drinkList.Add(d);
      }
      return drinkList;
    }
    #endregion
  }
}
