using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class ViewModel {
    #region Members
    private static ViewModel _Singleton = null;
    private Model.ScfService _Service = null;
    #endregion

    #region Properties
    public ICocktailFactory ScfService {
      get {
        return this._Service;
      }
    }
    #endregion

    #region Constructor
    public ViewModel() {
      this._Service = new Model.ScfService();
    }
    #endregion

    #region Public methods
    public static ViewModel Instance() {
      if (ViewModel._Singleton == null) {
        ViewModel._Singleton = new ViewModel();
      }
      return ViewModel._Singleton;
    }

    public ICocktailFactory GetService() {
      return this._Service;
    }
    #endregion
  }
}
