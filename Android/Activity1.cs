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
  [Activity(Label = "Smart Cocktail Factory", MainLauncher = true, Icon = "@drawable/SCF_Logo_Android_drawable")]
  public class Activity1 : Activity {
    #region Members
    private SignInViewModel _SignInViewModel;
    private bool _ConnectedActivityTriggered = false;
    #endregion

    #region Gui event handlers
    protected override void OnCreate(Bundle bundle) {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // set up button delegates
      FindViewById<Button>(Resource.Id.btnConnect).Click += delegate { this._Connect(); };
      FindViewById<Button>(Resource.Id.btnAbortConnect).Click += ButtonAbortConnection_Click;

      // set up view model
      this._SignInViewModel = new SignInViewModel();
      this._SignInViewModel.OnViewModelChanged += _SignInViewModel_OnSignInViewModelChanged;

      this._Connect();
    }

    protected override void OnDestroy() {
      base.OnDestroy();

      this._SignInViewModel.OnViewModelChanged -= _SignInViewModel_OnSignInViewModelChanged;
      this._SignInViewModel.DisposeViewModel();
    }

    protected override void OnResume() {
      base.OnResume();

      this._ConnectedActivityTriggered = false;
    }

    private void ButtonAbortConnection_Click(object sender, EventArgs e) {
      this._SetConnectingDependentWidgedVisibility(false);
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
    private void _Connect() {
      TextView tbxServiceUrl = FindViewById<TextView>(Resource.Id.tbxServiceUrl);
      TextView textConnectionState = FindViewById<TextView>(Resource.Id.lblConnectStatus);
      textConnectionState.SetText(Resource.String.Connecting);

      if(string.IsNullOrEmpty(tbxServiceUrl.Text)) {
        tbxServiceUrl.Text = "Please enter SCM URL";
        return;
      }

      this._SetConnectingDependentWidgedVisibility(true);
      this._SignInViewModel.RemoteUrl = tbxServiceUrl.Text;

      string welcomeMessage = this._SignInViewModel.WelcomeMessage;
      if (!string.IsNullOrEmpty(welcomeMessage)) {
        this._SetWelcomeMessage(welcomeMessage);
      }
    }

    private void _SetConnectingDependentWidgedVisibility(bool connecting) {
      TextView tbxServiceUrl = FindViewById<TextView>(Resource.Id.tbxServiceUrl);
      tbxServiceUrl.Enabled = !connecting;

      Button btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
      ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
      if (connecting == true) {
        progress.Visibility = ViewStates.Visible;
        btnConnect.Visibility = ViewStates.Invisible;
      } else {
        progress.Visibility = ViewStates.Invisible;
        btnConnect.Visibility = ViewStates.Visible;
      }
    }

    private void _SetWelcomeMessage(string message) {
      this._SetConnectingDependentWidgedVisibility(false);

      TextView textConnectionState = FindViewById<TextView>(Resource.Id.lblConnectStatus);

      if (!string.IsNullOrEmpty(message)) {
        textConnectionState.Text = message;
        this._TriggerDrinkListActivity();
        
      }
    }

    private void _TriggerDrinkListActivity() {
      if (this._ConnectedActivityTriggered == false) {
        StartActivity(typeof(DrinkListActivity));
        this._ConnectedActivityTriggered = true;
      }
    }
    #endregion
  }
}