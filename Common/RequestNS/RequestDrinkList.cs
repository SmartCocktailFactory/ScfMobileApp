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
      List<ViewModel.Drink> drinkList = new List<ViewModel.Drink>();
      string[] rawDrinks = this.Response.Replace("[", "").Replace("]", "").Replace("\"", "").Split(',');

      foreach (string s in rawDrinks) {
        ViewModel.Drink d = new ViewModel.Drink();
        d.Name = s.Trim(' ');
        drinkList.Add(d);
      }

      return drinkList;
    }
    #endregion

  }
}
