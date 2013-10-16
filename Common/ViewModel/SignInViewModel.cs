using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class SignInViewModel : IViewModel {
    #region Members
    private Model.ISignInService _SignInService;
    private string _CachedWelcomeMessage = string.Empty;
    #endregion

    #region Properties
    public string WelcomeMessage {
      get {
        if(string.IsNullOrEmpty(this._CachedWelcomeMessage)) {
          return this._SignInService.WelcomeMessage;
        } else {
          return this._CachedWelcomeMessage;
        }
      }
    }
    public string RemoteUrl {
      get {
        return Model.ModelFactory.Instance().RequestFactory.RemoteBaseUrl;
      }
      set {
        this._CachedWelcomeMessage = string.Empty;
        Model.ModelFactory.Instance().RequestFactory.RemoteBaseUrl = value;
      }
    }
    #endregion

    #region IViewModel
    public event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;

    public void DisposeViewModel() {
      this._SignInService.OnWelcomeMessageChanged -= _MyService_OnWelcomeMessageChanged;
    }
    #endregion

    #region Constructor
    public SignInViewModel() {
      this._SignInService = Model.ModelFactory.Instance().SignInService;
      this._SignInService.OnWelcomeMessageChanged += _MyService_OnWelcomeMessageChanged;
    }
    #endregion

    #region Event handlers
    void _MyService_OnWelcomeMessageChanged(object sender, Model.WelcomeMessageReceivedEventArgs e) {
      this._CachedWelcomeMessage = e.WelcomeMessage;

      if (this.OnViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
