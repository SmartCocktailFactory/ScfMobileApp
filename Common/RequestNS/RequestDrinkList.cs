using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestDrinkList : ARequest {

    #region Constructor
    public RequestDrinkList(IRequestExecutor executor)
      : base(executor) {
      this.RelativeUrl = "/cocktails";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

    #region Public methods
    public List<ViewModel.Drink> GetDrinks() {
      string[] drinks = this.Response.Split(',');

      List<ViewModel.Drink> drinkList = new List<ViewModel.Drink>();

      foreach (string s in drinks) {
        ViewModel.Drink d = new ViewModel.Drink();
        d.Name = s;
        drinkList.Add(d);

      }

      return drinkList;
    }
    #endregion

  }
}
