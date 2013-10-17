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
    private string _drinkId = null;
    private string _drinkName = null;
    private OrderViewModel _OrderViewModel;
    #endregion

    #region GUI event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.DrinkDetails);

      _drinkId = Intent.GetStringExtra("drinkId");
      _drinkName = Intent.GetStringExtra("drinkName");
      if (!string.IsNullOrEmpty(_drinkId)) {
        this.Title = _drinkId;

        Button orderDrink = FindViewById<Button>(Resource.Id.btnOrderDrink);
        orderDrink.Text = "Order " + _drinkName + " (" + _drinkId + ")";

        orderDrink.Click += ButtonOrderDrink_Click;

        this._OrderViewModel = new OrderViewModel();

        this._OrderViewModel.OnViewModelChanged += _OrderViewModel_OnViewModelChanged;


      } else {
        //activity is useless; finish it
        Console.WriteLine("can't get drinkId. Result: " +_drinkId);
        Finish();
      }
      // Create your application here
    }

    void _OrderViewModel_OnViewModelChanged(object sender, ViewModelChangedEventArgs e) {

            string sOrderMessage = "Last order, ID: ";
      sOrderMessage += this._OrderViewModel.CurrentOrder.OrderId;
      sOrderMessage += " Drink: ";
      sOrderMessage += this._OrderViewModel.CurrentOrder.DrinkName;
      
      RunOnUiThread(() => {
        TextView text = FindViewById<TextView>(Resource.Id.txtOrderResponse);
        text.Text = sOrderMessage;
      });

    }

    private void ButtonOrderDrink_Click(object sender, EventArgs e) {
      this._OrderViewModel.OrderDrink(_drinkId);    
    }
  #endregion

  }
}