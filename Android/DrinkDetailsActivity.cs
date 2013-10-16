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
  [Activity(Label = "Drink Details", Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class DrinkDetailsActivity : Activity {
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      if(!string.IsNullOrEmpty(Intent.GetStringExtra("drinkId"))) {
        this.Title = Intent.GetStringExtra("drinkId");
      }
      // Create your application here
    }
  }
}