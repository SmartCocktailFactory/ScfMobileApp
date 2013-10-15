using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class DrinkViewModel {

    #region Members
    private Model.ICocktailFactory _MyService;
    #endregion

    #region Properties
    public IList<string> DrinkNames {
      get {
        return this._MyService.DrinkNames;
      }
    }
    public IList<Drink> Drinks {
      get {
        return this._MyService.Drinks;
      }
    }
    #endregion

    #region Events
    public event EventHandler<ViewModelChangedEventArgs> OnDrinkViewModelChanged;
    #endregion

    #region Constructor
    public DrinkViewModel() {
      this._MyService = Model.ScfServiceFactory.Instance().ScfService;
      this._MyService.OnDrinkNamesChanged += _MyService_OnDrinkNamesChanged;
      this._MyService.OnDrinksChanged += _MyService_OnDrinksChanged;
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
