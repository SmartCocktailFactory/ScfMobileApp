using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinRtEST.Android.ViewModel {
  class RemoteService {

    #region Members
    private static RemoteService _Singleton = null;
    private Common.Service _Service = null;
    #endregion

    #region Properties
    public string RemoteUrl {
      get {
        if (this._Service != null) {
          return this._Service.BaseUrl;
        } else {
          return "";
        }
      }
      set {
        this._CreateRemote(value);
      }
    }
    #endregion

    #region Events
    public event EventHandler<RemoteServiceResponseEventArgs> OnWelcomeRequestCompleted;
    #endregion

    #region Public methods
    public static RemoteService instance() {
      if (_Singleton == null) {
        _Singleton = new RemoteService();
      }
      return _Singleton;
    }

    public void GetWelcomeRequest() {
      Common.RequestNS.IRequest request = this._Service.Factory.CreateWelcomeRequest();
      request.OnRequestCompleted += WelcomeRequest_OnRequestCompleted;
      request.Execute();
    }

    void WelcomeRequest_OnRequestCompleted(object sender, Common.RequestNS.RequestCompletedEventArgs e) {
      if (this.OnWelcomeRequestCompleted != null) {
        this.OnWelcomeRequestCompleted(this, new RemoteServiceResponseEventArgs(e.Request.Response, true));
      }
    }

    #endregion

    #region Private methods
    private void _CreateRemote(string remoteBaseUrl) {
      this._Service = new Common.Service(remoteBaseUrl);
    }
    #endregion

  }
}
