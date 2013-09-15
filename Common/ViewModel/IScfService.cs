using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  interface IScfService {
    #region Properties
    string WelcomeMessage { get; }
    IList<string> DrinkNames { get; }
    #endregion

    #region Events
    event EventHandler OnWelcomeMessageChanged;
    event EventHandler OnDrinksChanged;
    #endregion

    #region Methods
    void RequestWelcomeMessage();
    void RequestDrinkList();
    Drink GetDrinkDetails(string drinkName);
    void Order(string drinkName);
    #endregion
  }
}
