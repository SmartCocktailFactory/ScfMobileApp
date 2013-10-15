using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class SignInViewModel {
    #region Members
    private Model.ICocktailFactory _MyService;
    #endregion

    #region Properties
    public string WelcomeMessage {
      get {
        return this._MyService.WelcomeMessage;
      }
    }
    public string RemoteUrl {
      get {
        return this._MyService.ScfRemoteUrl;
      }
      set {
        this._MyService.ScfRemoteUrl = value;
      }
    }
    #endregion

    #region Events
    public event EventHandler<ViewModelChangedEventArgs> OnSignInViewModelChanged;
    #endregion

    #region Constructor
    public SignInViewModel() {
      this._MyService = Model.ScfServiceFactory.Instance().ScfService;

      this._MyService.OnWelcomeMessageChanged += _MyService_OnWelcomeMessageChanged;
    }
    #endregion

    #region Event handlers
    void _MyService_OnWelcomeMessageChanged(object sender, Model.WelcomeMessageReceivedEventArgs e) {
      if (this.OnSignInViewModelChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnSignInViewModelChanged(this, new ViewModelChangedEventArgs());
        });
      }
    }
    #endregion
  }
}
