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
    #region GUI event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkList);

      ViewModel.Instance().ScfService.OnDrinkNamesChanged += ScfService_OnDrinkNamesChanged;
      ViewModel.Instance().ScfService.OnOrderChanged += ScfService_OnOrderChanged;

      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.ItemClick += view_ItemClick;

      this._SetDrinkList(ViewModel.Instance().ScfService.DrinkNames);
    }

    void view_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      ViewModel.Instance().ScfService.OrderDrink(view.Adapter.GetItem(e.Position).ToString());
    }
    #endregion

    #region Model event handlers
    void ScfService_OnOrderChanged(object sender, OrderChangedEventArgs e) {
      RunOnUiThread(() => {
        TextView text = FindViewById<TextView>(Resource.Id.txtLastOrder);
        text.Text = "Last order, ID: " + e.Order.OrderId + " Drink: " + e.Order.DrinkName;
      });
    }

    void ScfService_OnDrinkNamesChanged(object sender, DrinkNamesChangedEventArgs e) {
      RunOnUiThread(() => {
        this._SetDrinkList(e.DrinkNames);
      });
    }

    private void _SetDrinkList(IList<string> drinkNames) {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.Adapter = new ArrayAdapter(this, Android.Resource.Layout.ListViewDataItems, drinkNames.ToArray());
    }
    #endregion
  }
}