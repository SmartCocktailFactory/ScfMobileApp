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
    private static string _RemoteAddress = "http://192.168.1.40:12345";
    #endregion

    #region Properties
    public Common.ViewModel.SignInViewModel SignInViewModel { get; private set; }
    #endregion

    public SignInView() {
      InitializeComponent();

      this.SignInViewModel = new Common.ViewModel.SignInViewModel();
    }

    #region Events
    public event EventHandler OnConnected;
    public event EventHandler OnConnecting;
    #endregion

    #region GUI event handler
    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
      this.DataContext = this.SignInViewModel;

      this._SetConnecting();
      this.tbxRemoteAddress.Text = _RemoteAddress;
      this.tbxRemoteAddress.IsEnabled = false;
      this.tbxWelcomeMessage.Text = "none";
      this.SignInViewModel.OnViewModelChanged += _mySignInViewModel_OnViewModelChanged;

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
        this.tbxWelcomeMessage.Text = this.SignInViewModel.WelcomeMessage;
      });

      if (this.OnConnected != null) {
        this.OnConnected(this, new EventArgs());
      }

      this.Dispatcher.Invoke(delegate {
        this._StopConnecting();
      });
    }
    #endregion

    #region Private methods
    private void _Connect() {
      if (this.OnConnecting != null) {
        this.OnConnecting(this, new EventArgs());
      }

      this._SetConnecting();

      Common.Model.ModelFactory.Instance().ResetServices();

      this.SignInViewModel.RemoteUrl = this.tbxRemoteAddress.Text;
      this.tbxWelcomeMessage.Text = this.SignInViewModel.WelcomeMessage;
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
