using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public List<ViewModel.Drink> GetDrinks() {
      List<ViewModel.Drink> drinkList = new List<ViewModel.Drink>();
      string[] rawDrinks = this.Response.Replace("\n", "").Replace(" ", "")
        .Replace("[", "").Replace("]", "")
        .Replace("{", "")
        .Replace("\"", "").Split(new string[] {"},"}, StringSplitOptions.RemoveEmptyEntries);

      foreach (string s in rawDrinks) {
        ViewModel.Drink d = new ViewModel.Drink();
        string[] properties = s.Replace("}", "").Split(',');

        d.DrinkId = properties[0].Remove(0, properties[0].IndexOf(':') + 1);
        d.Name = properties[1].Remove(0, properties[1].IndexOf(':') + 1);
        drinkList.Add(d);
      }
      return drinkList;
    }
    #endregion
  }
}
