using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  interface ICocktailFactory {
    #region Properties
    string WelcomeMessage { get; }
    IList<string> DrinkNames { get; }
    IList<Drink> Drinks { get; }
    Order CurrentOrder { get; }
    string ScfRemoteUrl { get; set; }
    #endregion

    #region Events
    event EventHandler<WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;
    event EventHandler<DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    event EventHandler<DrinksChangedEventArgs> OnDrinksChanged;
    event EventHandler<OrderChangedEventArgs> OnOrderChanged;
    #endregion
  }
}
