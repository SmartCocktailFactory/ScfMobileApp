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

using Common.ViewModel;

namespace ScfMobileApp.Android {
  [Activity(Label = "Drink list")]
  public class DrinkListActivity : Activity {
    #region Members
    private DrinkViewModel _DrinkViewModel;
    private OrderViewModel _OrderViewModel;
    #endregion

    #region GUI event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkList);

      // set up view models
      this._DrinkViewModel = new DrinkViewModel();
      this._DrinkViewModel.OnDrinkViewModelChanged += _DrinkViewModel_OnDrinkViewModelChanged;
      this._OrderViewModel = new OrderViewModel();
      this._OrderViewModel.OnOrderViewModelChanged += _OrderViewModel_OnOrderViewModelChanged;
      

      // set up gui elements      
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.ItemClick += view_ItemClick;

      this._SetDrinkList(this._DrinkViewModel.DrinkNames);
    }

    void view_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      this._OrderViewModel.OrderDrink(view.Adapter.GetItem(e.Position).ToString());
    }
    #endregion

    #region Model event handlers
    void _OrderViewModel_OnOrderViewModelChanged(object sender, ViewModelChangedEventArgs e) {
      string sOrderMessage = "Last order, ID: ";
      sOrderMessage += this._OrderViewModel.CurrentOrder.OrderId;
      sOrderMessage += " Drink: ";
      sOrderMessage += this._OrderViewModel.CurrentOrder.DrinkName;
      
      RunOnUiThread(() => {
        TextView text = FindViewById<TextView>(Resource.Id.txtLastOrder);
        text.Text = sOrderMessage;
      });
    }

    void _DrinkViewModel_OnDrinkViewModelChanged(object sender, ViewModelChangedEventArgs e) {
      IList<string> drinkNames = this._DrinkViewModel.DrinkNames;
      RunOnUiThread(() => {
        this._SetDrinkList(drinkNames);
      });
    }

    private void _SetDrinkList(IList<string> drinkNames) {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.Adapter = new ArrayAdapter(this, Android.Resource.Layout.ListViewDataItems, drinkNames.ToArray());
    }
    #endregion
  }
}