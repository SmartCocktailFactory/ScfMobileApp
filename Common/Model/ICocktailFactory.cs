using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  interface ICocktailFactory {
    #region Properties
    string WelcomeMessage { get; }
    IList<string> DrinkNames { get; }
    IList<ViewModel.Drink> Drinks { get; }
    ViewModel.Order CurrentOrder { get; }
    string ScfRemoteUrl { get; set; }
    #endregion

    #region Events
    event EventHandler<WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;
    event EventHandler<DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    event EventHandler<DrinksChangedEventArgs> OnDrinksChanged;
    event EventHandler<OrderChangedEventArgs> OnOrderChanged;
    #endregion

    #region Methods
    void OrderDrink(string drinkId);
    #endregion
  }
}
