using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  interface IDrinkService {
    #region Properties
    IList<string> DrinkNames { get; }
    IList<DTO.Drink> Drinks { get; }
    #endregion

    #region Events
    event EventHandler<DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    event EventHandler<DrinksChangedEventArgs> OnDrinksChanged;
    #endregion
  }
}
