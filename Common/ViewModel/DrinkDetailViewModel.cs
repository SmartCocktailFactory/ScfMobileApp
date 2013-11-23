using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  public class DrinkDetailViewModel : IViewModel {

    #region Members
    private Common.Model.IOrderService _OrderService;
    private Common.Model.IDrinkService _DrinkService;
    #endregion

    #region Properties
    #endregion

    #region Constructor
    public DrinkDetailViewModel() {
      this._DrinkService = Model.ModelFactory.Instance().DrinkService;
      this._DrinkService.OnDrinksChanged += _DrinkService_OnDrinksChanged;

      this._OrderService = Model.ModelFactory.Instance().OrderService;
    }
    #endregion

    #region IViewModel
    public event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;

    public void DisposeViewModel() {
      this._DrinkService.OnDrinksChanged -= this._DrinkService_OnDrinksChanged;
    }
    #endregion

    #region Public methods
    public DTO.Drink GetDrink(string drinkId) {
      return this._DrinkService.GetDrink(drinkId);
    }
    public void OrderDrink(string drinkId) {
      this._OrderService.OrderDrink(drinkId);
    }
    #endregion

    #region Event handlers
    void _DrinkService_OnDrinksChanged(object sender, Model.DrinksChangedEventArgs e) {
      this._NotifyViewModelChanged();
    }
    #endregion

    #region Private mehtods
    private void _NotifyViewModelChanged() {
      if (this.OnViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
