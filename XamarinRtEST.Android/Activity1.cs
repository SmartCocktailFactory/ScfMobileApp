using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinRtEST.Android {
  [Activity(Label = "XamarinRtEST.Android", MainLauncher = true, Icon = "@drawable/icon")]
  public class Activity1 : Activity {

    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.btnGetDrinks);
      button.Click += delegate { this._OnRequestDrinks(); };
    }

    private void _OnRequestDrinks() {
      EditText tbxServiceUrl = FindViewById<EditText>(Resource.Id.tbxServiceUrl);

      if(string.IsNullOrEmpty(tbxServiceUrl.Text)) {
        tbxServiceUrl.Text = "Please enter SCM URL";
        return;
      }

      ViewModel.RemoteService.instance().RemoteUrl = tbxServiceUrl.Text;
      EditText response = FindViewById<EditText>(Resource.Id.tbxResponse);
      response.Text = ViewModel.RemoteService.instance().GetWelcomeRequest();      
    }
  }
}

