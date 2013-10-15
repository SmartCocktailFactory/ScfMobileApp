using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Common.Model{
    class ModelFactory {

    #region Members
    private static ModelFactory _Singleton = null;
    #endregion

    #region Properties
    public ScfDrinkService DrinkService { get; private set;}
    public ScfOrderService OrderService { get; private set; }
    public ScfSignInService SignInService { get; private set; }
    public RequestExecutor Executor { get; private set;}
    #endregion

    #region Constructor
    public ModelFactory() {
        DrinkService = new ScfDrinkService();
        OrderService = new ScfOrderService();
        SignInService = new ScfSignInService();
    }
    #endregion

    #region Public methods
    public static ModelFactory Instance() {
      if (ModelFactory._Singleton == null) {
        ModelFactory._Singleton = new ModelFactory();
      }
      return ModelFactory._Singleton;
    }
    #endregion
    }
}
