using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class OrderViewModel  : IViewModel {

    #region Members
    private Model.IOrderService _OrderService = null;
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
    public event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;
    public void DisposeViewModel() {
      this._OrderService.OnOrderChanged -= this._MyService_OnOrderChanged;
    }
    #endregion

    #region Constructor
    public OrderViewModel() {
      this._OrderService = Model.ModelFactory.Instance().OrderService;
      this._OrderService.OnOrderChanged += _MyService_OnOrderChanged;
    }
    #endregion

    #region Public methods
    public void OrderDrink(string drinkId) {
      this._OrderService.OrderDrink(drinkId);
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
      if (this.OnViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
