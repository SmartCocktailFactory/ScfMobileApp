using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class ViewModel {
    #region Members
    private static ViewModel _Singleton = null;
    private SCFService _Service = null;
    #endregion

    #region Properties
    public string ScfUrl {
      get {
        if (this._Service == null) {
          return "";
        }
        return this._Service.RemoteUrl;
      }
      set {
        this.SetRemoteUrl(value);
      }
    }
    public IScfService _ScfService {
      get {
        return this._Service;
      }
    }
    #endregion

    #region Constructor
    public ViewModel() {
      this._Service = new SCFService();
    }
    #endregion

    #region Public methods
    public static ViewModel Instance() {
      if (ViewModel._Singleton == null) {
        ViewModel._Singleton = new ViewModel();
      }
      return ViewModel._Singleton;
    }

    public IScfService GetService() {
      return this._Service;
    }
    #endregion

    #region Private methods
    private void SetRemoteUrl(string url) {
      if (this._Service == null) {
        this._Service = new SCFService();
      }
      this._Service.RemoteUrl = url;
    }
    #endregion

  }
}
