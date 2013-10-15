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
    #region Members
    private SignInViewModel _SignInViewModel;
    #endregion

    #region Gui event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // set up button delegates
      FindViewById<Button>(Resource.Id.btnConnect).Click += delegate { this._OnConnect(); };
      FindViewById<Button>(Resource.Id.btnGetDrinks).Click += delegate { StartActivity(typeof(DrinkListActivity)); };

      // set up view model
      this._SignInViewModel = new SignInViewModel();
      this._SignInViewModel.OnSignInViewModelChanged += _SignInViewModel_OnSignInViewModelChanged;
    }
    #endregion

    #region Model event handlers
    void _SignInViewModel_OnSignInViewModelChanged(object sender, Common.ViewModel.ViewModelChangedEventArgs e) {
      string message = this._SignInViewModel.WelcomeMessage;
      RunOnUiThread(() => {
        this._SetWelcomeMessage(message);
      });
    }
    #endregion

    #region Private methods
    private void _OnConnect() {
      TextView tbxServiceUrl = FindViewById<TextView>(Resource.Id.tbxServiceUrl);

      if(string.IsNullOrEmpty(tbxServiceUrl.Text)) {
        tbxServiceUrl.Text = "Please enter SCM URL";
        return;
      }
      this._SignInViewModel.RemoteUrl = tbxServiceUrl.Text;
      this._SetWelcomeMessage(this._SignInViewModel.WelcomeMessage);
    }

    private void _SetWelcomeMessage(string message) {
      TextView response = FindViewById<TextView>(Resource.Id.tbxResponse);
      response.Text = message;

      if (!string.IsNullOrEmpty(message)) {
        FindViewById<Button>(Resource.Id.btnGetDrinks).Visibility = ViewStates.Visible;
      }
    }
    #endregion
  }
}

