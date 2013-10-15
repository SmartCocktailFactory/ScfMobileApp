using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class SignInViewModel : IViewModel {
    #region Members
    private Model.ISignInService _SignInService;
    #endregion

    #region Properties
    public string WelcomeMessage {
      get {
        return this._SignInService.WelcomeMessage;
      }
    }
    public string RemoteUrl {
      get {
        return this._SignInService.ScfRemoteUrl;
      }
      set {
        this._SignInService.ScfRemoteUrl = value;
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
      if (this.OnViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
