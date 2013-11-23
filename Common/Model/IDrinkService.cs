using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  interface IDrinkService {
    #region Properties
    IList<DTO.Drink> Drinks { get; }
    IList<string> DrinkNames { get; }
    #endregion

    #region Public methods
    DTO.Drink GetDrink(string drinkId);
    #endregion

    #region Events
    event EventHandler<DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    event EventHandler<DrinksChangedEventArgs> OnDrinksChanged;
    #endregion
  }
}
