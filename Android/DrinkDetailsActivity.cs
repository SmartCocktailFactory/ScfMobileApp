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
  [Activity(Label = "Drink Details", Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class DrinkDetailsActivity : Activity {
    
    #region Members
    private string _DrinkId = null;
    private DrinkDetailViewModel _DrinkDetailsViewModel;
    #endregion

    #region GUI event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkDetails);

      this._DrinkDetailsViewModel = new DrinkDetailViewModel();
      this._DrinkDetailsViewModel.OnViewModelChanged += _DrinkDetailsViewModel_OnViewModelChanged;

      this._DrinkId = Intent.GetStringExtra("drinkId");

      if(!string.IsNullOrEmpty(_DrinkId)) {
        Button orderDrink = FindViewById<Button>(Resource.Id.btnOrderDrink);
        orderDrink.Click += ButtonOrderDrink_Click;

        this._UpdateDrink();
      }
    }

    protected override void OnDestroy() {
      base.OnDestroy();

      this._DrinkDetailsViewModel.OnViewModelChanged -= _DrinkDetailsViewModel_OnViewModelChanged;
      this._DrinkDetailsViewModel.DisposeViewModel();
    }

    void _DrinkDetailsViewModel_OnViewModelChanged(object sender, ViewModelChangedEventArgs e) {
      this.RunOnUiThread(() => {
        this._UpdateDrink();
      });
    }

    private void ButtonOrderDrink_Click(object sender, EventArgs e) {
      this._DrinkDetailsViewModel.OrderDrink(this._DrinkId);
      this._TriggerOrderistActivity();
    }
    #endregion

    #region Private mehtods
    private void _UpdateDrink() {
      Common.DTO.Drink drink = this._DrinkDetailsViewModel.GetDrink(this._DrinkId);
      this.Title = drink.Name;
      TextView txtDrinkDetails = FindViewById<TextView>(Resource.Id.txtDrinkDetails);
      txtDrinkDetails.Text = drink.Description;
      txtDrinkDetails.Text += "\nRecipe:" + drink.Recipe.Replace("[", "").Replace("]", "").Replace(",", "").Replace("\"", "");
      ;

      Button orderDrink = FindViewById<Button>(Resource.Id.btnOrderDrink);

      string drinkName = drink.Name;
      if (drinkName.Contains('(')) {
        drinkName = drinkName.Remove(drinkName.IndexOf('(') - 1);
      }

      orderDrink.Text = "Order '" + drinkName + "'";
    }

    private void _TriggerOrderistActivity() {
      Intent orderIntend = new Intent(this, typeof(OrderListActivity));

      StartActivity(orderIntend);
    }

    #endregion
  }
}