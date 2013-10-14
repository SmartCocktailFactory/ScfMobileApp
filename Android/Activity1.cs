using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Common.ViewModel;
using System.Collections.Generic;

namespace ScfMobileApp.Android {
  [Activity(Label = "Smart Cocktail Facotry", MainLauncher = true, Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class Activity1 : Activity {

    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.btnConnect);
      button.Click += delegate { this._OnConnect(); };

      FindViewById<Button>(Resource.Id.btnGetDrinks).Click += delegate { StartActivity(typeof(DrinkListActivity)); };


      ViewModel.Instance().ScfService.OnWelcomeMessageChanged += _ScfService_OnWelcomeMessageChanged;
    }

    void _ScfService_OnWelcomeMessageChanged(object sender, WelcomeMessageReceivedEventArgs e) {
      RunOnUiThread(() => {
        this._SetWelcomeMessage(e.WelcomeMessage);
      });
    }
    private void _OnConnect() {
      TextView tbxServiceUrl = FindViewById<TextView>(Resource.Id.tbxServiceUrl);

      if(string.IsNullOrEmpty(tbxServiceUrl.Text)) {
        tbxServiceUrl.Text = "Please enter SCM URL";
        return;
      }
      ViewModel.Instance().ScfService.ScfRemoteUrl = tbxServiceUrl.Text;
      this._SetWelcomeMessage(ViewModel.Instance().ScfService.WelcomeMessage);
    }

    private void _SetWelcomeMessage(string message) {
      TextView response = FindViewById<TextView>(Resource.Id.tbxResponse);
      response.Text = message;

      if (!string.IsNullOrEmpty(message)) {
        FindViewById<Button>(Resource.Id.btnGetDrinks).Visibility = ViewStates.Visible;
      }
    }
  }
}

