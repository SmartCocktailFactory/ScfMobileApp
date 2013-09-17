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

namespace XamarinRtEST.Android {
  [Activity(Label = "Drink list")]
  public class DrinkListActivity : Activity {
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkList);

      ViewModel.Instance().ScfService.OnDrinkNamesChanged += ScfService_OnDrinkNamesChanged;

      this._SetDrinkList(ViewModel.Instance().ScfService.DrinkNames);
    }

    void ScfService_OnDrinkNamesChanged(object sender, DrinkNamesChangedEventArgs e) {
      RunOnUiThread(() => {
        this._SetDrinkList(e.DrinkNames);
      });
    }

    private void _SetDrinkList(IList<string> drinkNames) {
      TextView text = FindViewById<TextView>(Resource.Id.drinkTextView);
      
      
      text .Text = string.Empty;

      foreach (string name in drinkNames) {
        text .Text += name + "\r\n";
      }
    }
  }
}