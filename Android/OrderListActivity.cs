using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ScfMobileApp.Android {
  [Activity(Label = "Order List", Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class OrderListActivity : Activity {
    #region Members
    private Common.ViewModel.OrderViewModel _OrderViewModel;
    private List<Common.ViewModel.OrderDetails> _DetailedOrderList = new List<Common.ViewModel.OrderDetails>();
    #endregion

    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      this.SetContentView(Resource.Layout.OrderList);

      this._OrderViewModel = new Common.ViewModel.OrderViewModel();
      this._OrderViewModel.OnViewModelChanged += _OrderViewModel_OnViewModelChanged;

      this._DetailedOrderList = this._OrderViewModel.DetailedOrders.ToList();
      this._UpdateOrderList();
    }

    protected override void OnDestroy() {
      base.OnDestroy();

      this._OrderViewModel.DisposeViewModel();
    }

    #region Event handlers
    void _OrderViewModel_OnViewModelChanged(object sender, Common.ViewModel.ViewModelChangedEventArgs e) {
      this.RunOnUiThread(() => {
        this._UpdateOrderList();
      });
    }
    #endregion

    #region Private methods
    private void _UpdateOrderList() {
      this._DetailedOrderList = this._OrderViewModel.DetailedOrders.ToList();
      ListView view = FindViewById<ListView>(Resource.Id.orderListView);
      view.Adapter = new OrderDetailListAdapter(this, this._DetailedOrderList);
    }
    #endregion
  }
}