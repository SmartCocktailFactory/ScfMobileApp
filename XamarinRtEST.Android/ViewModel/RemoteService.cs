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

    #region Public methods
    public static RemoteService instance() {
      if (_Singleton == null) {
        _Singleton = new RemoteService();
      }
      return _Singleton;
    }

    public string GetWelcomeRequest() {
      Common.Request request = new Common.RequestWelcome();
      this._Service.RunRequest(request);

      return request.Response;
    }

    #endregion

    #region Private methods
    private void _CreateRemote(string remoteBaseUrl) {
      this._Service = new Common.Service(remoteBaseUrl);
    }
    #endregion

  }
}
