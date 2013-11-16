using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  public class DrinkViewModel : IViewModel {

    #region Members
    private Model.IDrinkService _DrinkService = null;
    #endregion

    #region Properties
    public IList<DTO.Drink> Drinks {
      get {
        return this._DrinkService.Drinks;
      }
    }

    public IList<string> DrinkNames {
      get {
        return this._DrinkService.DrinkNames;
      }
    }
    #endregion

    #region Public mehtods

    #endregion

    #region Constructor
    public DrinkViewModel() {
      this._DrinkService = Model.ModelFactory.Instance().DrinkService;
      this._DrinkService.OnDrinkNamesChanged += _MyService_OnDrinkNamesChanged;
    }
    #endregion

    #region IViewModel
    public event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;

    public void DisposeViewModel() {
      this._DrinkService.OnDrinkNamesChanged -= this._MyService_OnDrinkNamesChanged;
    }
    #endregion

    #region Event handlers
    void _MyService_OnDrinkNamesChanged(object sender, Model.DrinkNamesChangedEventArgs e) {
      this._NotifyViewModelChanged();
    }
    #endregion

    #region Private methods
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
