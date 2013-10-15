using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class DrinkViewModel {

    #region Members
    private Model.IDrinkService _DrinkService = null;
    #endregion

    #region Properties
    public IList<string> DrinkNames {
      get {
        return this._DrinkService.DrinkNames;
      }
    }
    public IList<Drink> Drinks {
      get {
        return this._DrinkService.Drinks;
      }
    }
    #endregion

    #region Events
    public event EventHandler<ViewModelChangedEventArgs> OnDrinkViewModelChanged;
    #endregion

    #region Constructor
    public DrinkViewModel() {
      this._DrinkService = Model.ModelFactory.Instance().DrinkService;
      this._DrinkService.OnDrinkNamesChanged += _MyService_OnDrinkNamesChanged;
      this._DrinkService.OnDrinksChanged += _MyService_OnDrinksChanged;
    }
    #endregion

    #region Event handlers
    void _MyService_OnDrinksChanged(object sender, Model.DrinksChangedEventArgs e) {
      this._NotifyViewModelChanged();
    }

    void _MyService_OnDrinkNamesChanged(object sender, Model.DrinkNamesChangedEventArgs e) {
      this._NotifyViewModelChanged();
    }
    #endregion

    #region Private methods
    private void _NotifyViewModelChanged() {
      if (this.OnDrinkViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnDrinkViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
