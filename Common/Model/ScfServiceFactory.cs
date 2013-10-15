using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class ScfServiceFactory {
    #region Members
    private static ScfServiceFactory _Singleton = null;
    private ScfService _MyService = null;
    #endregion

    #region Properties
    public ICocktailFactory ScfService {
      get {
        return this._MyService;
      }
    }
    #endregion

    #region Constructor
    public ScfServiceFactory() {
      this._MyService = new ScfService();
    }
    #endregion

    #region Public methods
    public static ScfServiceFactory Instance() {
      if (ScfServiceFactory._Singleton == null) {
        ScfServiceFactory._Singleton = new ScfServiceFactory();
      }
      return ScfServiceFactory._Singleton;
    }
    #endregion
  }
}
