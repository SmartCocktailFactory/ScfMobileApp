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
  [Activity(Label = "Drink List", Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class DrinkListActivity : Activity {
    #region Members
    private DrinkViewModel _DrinkViewModel;
    private List<Common.DTO.Drink> _Drinks = new List<Common.DTO.Drink>();
    #endregion

    #region GUI event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkList);

      // set up view models
      this._DrinkViewModel = new DrinkViewModel();
      this._DrinkViewModel.OnViewModelChanged += _DrinkViewModel_OnDrinkViewModelChanged;
      

      // set up gui elements
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.ItemClick += view_ItemClick;

      this._Drinks = this._DrinkViewModel.Drinks.ToList();
      this._SetDrinkList();
    }

    protected override void OnDestroy() {
      base.OnDestroy();

      this._DrinkViewModel.OnViewModelChanged -= this._DrinkViewModel_OnDrinkViewModelChanged;
      this._DrinkViewModel.DisposeViewModel();
    }

    void view_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      Common.DTO.Drink drink = this._Drinks[e.Position];
      this._TriggerDrinkDetailsActivity(drink.DrinkId);
    }
    #endregion

    #region Model event handlers
    void _DrinkViewModel_OnDrinkViewModelChanged(object sender, ViewModelChangedEventArgs e) {
      this._Drinks = this._DrinkViewModel.Drinks.ToList();
      this.RunOnUiThread(() => {
        this._SetDrinkList();
      });
    }

    private void _SetDrinkList() {
      ListView view = FindViewById<ListView>(Resource.Id.drinkListView);
      view.Adapter = new DrinkListAdapter(this, this._Drinks);
    }
    #endregion

    #region Private methods
    private void _TriggerDrinkDetailsActivity(string drinkId) {
      Intent drinkIntend = new Intent(this, typeof(DrinkDetailsActivity));
      drinkIntend.PutExtra("drinkId", drinkId);

      StartActivity(drinkIntend);
    }
    #endregion
  }
}