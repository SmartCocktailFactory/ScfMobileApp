using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScfApp.SimGui {
  /// <summary>
  /// Interaction logic for SignInView.xaml
  /// </summary>
  public partial class SignInView : UserControl {

    #region Constants
    private static string _RemoteAddress = "http://192.168.1.35:12345";
    #endregion

    #region Members
    public Common.ViewModel.SignInViewModel _mySignInViewModel = new Common.ViewModel.SignInViewModel();
    #endregion

    public SignInView() {
      InitializeComponent();
    }

    #region GUI event handler
    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      this.DataContext = this._mySignInViewModel;

      this._SetConnecting();
      this.tbxRemoteAddress.Text = _RemoteAddress;
      this.tbxRemoteAddress.IsEnabled = false;
      this.tbxWelcomeMessage.Text = "none";
      this._mySignInViewModel.OnViewModelChanged += _mySignInViewModel_OnViewModelChanged;

      this._Connect();
    }

    private void btnAbort_Click(object sender, RoutedEventArgs e) {
      this._StopConnecting();
    }

    private void btnConnect_Click(object sender, RoutedEventArgs e) {
      this._Connect();
    }

    #endregion

    #region Model event handler
    void _mySignInViewModel_OnViewModelChanged(object sender, Common.ViewModel.ViewModelChangedEventArgs e) {
      this.Dispatcher.Invoke(delegate {
        this.tbxWelcomeMessage.Text = this._mySignInViewModel.WelcomeMessage;
      });
    }
    #endregion

    #region Private methods
    private void _Connect() {
      this._SetConnecting();

      Common.Model.ModelFactory.Instance().ResetServices();

      this._mySignInViewModel.RemoteUrl = this.tbxRemoteAddress.Text;
      this.tbxWelcomeMessage.Text = this._mySignInViewModel.WelcomeMessage;
    }

    private void _SetConnecting() {
      this.btnConnect.IsEnabled = false;
      this.tbxRemoteAddress.IsEnabled = false;
    }

    private void _StopConnecting() {
      this.btnConnect.IsEnabled = true;
      this.tbxRemoteAddress.IsEnabled = true;
    }
    #endregion
  }
}
