using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
  interface IOrderService {
    #region Properties
    ViewModel.Order CurrentOrder { get; }
    #endregion

    #region Events
    event EventHandler<OrderChangedEventArgs> OnOrderChanged;
    #endregion

    #region Methods
    void OrderDrink(string drinkId);
    #endregion
  }
}
