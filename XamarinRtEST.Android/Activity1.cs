using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Common.ViewModel;
using System.Collections.Generic;

namespace XamarinRtEST.Android {
  [Activity(Label = "XamarinRtEST.Android", MainLauncher = true, Icon = "@drawable/icon")]
  public class Activity1 : Activity {

    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.btnConnect);
      button.Click += delegate { this._OnConnect(); };

      FindViewById<Button>(Resource.Id.btnGetDrinks).Click += delegate { this._OnGetDrinks(); };


      ViewModel.Instance().ScfService.OnWelcomeMessageChanged += _ScfService_OnWelcomeMessageChanged;
      ViewModel.Instance().ScfService.OnDrinkNamesChanged += ScfService_OnDrinkNamesChanged;
    }

    void _ScfService_OnWelcomeMessageChanged(object sender, WelcomeMessageReceivedEventArgs e) {
      RunOnUiThread(() => {
        this._SetWelcomeMessage(e.WelcomeMessage);
      });
    }

    void ScfService_OnDrinkNamesChanged(object sender, DrinkNamesChangedEventArgs e) {
      RunOnUiThread(() => {
        this._SetDrinkList(e.DrinkNames);
      });
    }

    private void _OnConnect() {
      EditText tbxServiceUrl = FindViewById<EditText>(Resource.Id.tbxServiceUrl);

      if(string.IsNullOrEmpty(tbxServiceUrl.Text)) {
        tbxServiceUrl.Text = "Please enter SCM URL";
        return;
      }
      ViewModel.Instance().ScfService.ScfRemoteUrl = tbxServiceUrl.Text;
      this._SetWelcomeMessage(ViewModel.Instance().ScfService.WelcomeMessage);
    }

    private void _OnGetDrinks() {
      this._SetDrinkList(ViewModel.Instance().ScfService.DrinkNames);
    }

    private void _SetWelcomeMessage(string message) {
      EditText response = FindViewById<EditText>(Resource.Id.tbxResponse);
      response.Text = message;

      if (!string.IsNullOrEmpty(message)) {
        FindViewById<Button>(Resource.Id.btnGetDrinks).Visibility = ViewStates.Visible;
      }
    }

    private void _SetDrinkList(IList<string> drinkNames) {
      EditText response = FindViewById<EditText>(Resource.Id.tbxResponse);
      response.Text = string.Empty;

      foreach (string name in drinkNames) {
        response.Text += name + "\r\n";
      }

    }
  }
}

