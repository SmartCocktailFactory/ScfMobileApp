using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class OrderViewModel {

    #region Members
    private Model.ICocktailFactory _MyService = null;
    private Order _CurrentOrder = new Order();
    private object _OrderLock = new object();
    #endregion

    #region Properties
    public Order CurrentOrder {
      get {
        lock (this._OrderLock) {
          return _CurrentOrder;
        }
      }
    }
    #endregion

    #region Events
    public event EventHandler<ViewModelChangedEventArgs> OnOrderViewModelChanged;
    #endregion

    #region Constructor
    public OrderViewModel() {
      this._MyService = Model.ScfServiceFactory.Instance().ScfService;
      this._MyService.OnOrderChanged += _MyService_OnOrderChanged;
    }
    #endregion

    #region Public methods
    public void OrderDrink(string drinkId) {
      this._MyService.OrderDrink(drinkId);
    }
    #endregion

    #region Event handlers
    void _MyService_OnOrderChanged(object sender, Model.OrderChangedEventArgs e) {
      lock (this._OrderLock) {
        this._CurrentOrder = e.Order;
      }

      this._NotifyModelChanged();
    }
    #endregion

    #region Private methods
    private void _NotifyModelChanged() {
      if (this.OnOrderViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnOrderViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
