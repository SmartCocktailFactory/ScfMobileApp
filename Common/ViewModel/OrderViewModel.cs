﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class OrderViewModel  : IViewModel {

    #region Members
    private Model.IOrderService _OrderService = null;
		private DTO.Order _CurrentOrder = null;
    private object _OrderLock = new object();
    #endregion

    #region Properties
		public DTO.Order CurrentOrder {
      get {
        lock (this._OrderLock) {
          return _CurrentOrder;
        }
      }
    }
    public IList<DTO.Order> PendingOrders {
      get {
        return this._OrderService.CurrentOrders.Where(x => x.OrderStateId == DTO.StateId.Pending).ToList();
      }
    }

    public IList<DTO.Order> CompletedOrders {
      get {
        return this._OrderService.CurrentOrders.Where(x => x.OrderStateId == DTO.StateId.Completed).ToList();
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
      this._UpdateCurrentOrder(e.Order);

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

    private void _UpdateCurrentOrder(DTO.Order incomminCorder) {
      if (this._CurrentOrder != null) {
        if(this._CurrentOrder.OrderId == incomminCorder.OrderId) {
          if (incomminCorder.OrderStateId != DTO.StateId.InProgress) {
            this._CurrentOrder = null;
          }
        }
      }

      if (incomminCorder.OrderStateId == DTO.StateId.InProgress) {
        this._CurrentOrder = incomminCorder;
      }
    }

    #endregion
  }
}
