using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Common.Model{
  public class ModelFactory {

    #region Members
    private static ModelFactory _Singleton = null;
    #endregion

    #region Properties
    public ScfDrinkService DrinkService { get; private set; }
    public ScfOrderService OrderService { get; private set; }
    public ScfSignInService SignInService { get; private set; }
    public RequestExecutor Executor { get; private set; }
    public RequestNS.RequestFactory RequestFactory { get; private set; }
    #endregion

    #region Constructor
    public ModelFactory() {
      this.Executor = new RequestExecutor();
      this.RequestFactory = new RequestNS.RequestFactory(this.Executor);
      this.DrinkService = new ScfDrinkService(this.RequestFactory);
      this.OrderService = new ScfOrderService(this.RequestFactory);
      this.SignInService = new ScfSignInService(this.RequestFactory);
    }
    #endregion

    #region Public methods
    public static ModelFactory Instance() {
      if (ModelFactory._Singleton == null) {
        ModelFactory._Singleton = new ModelFactory();
      }
      return ModelFactory._Singleton;
    }

    public void ResetServices() {
      this.DrinkService.ResetService();
      this.OrderService.ResetService();
      this.SignInService.ResetService();
    }

    public void Dispose() {
      this.ResetServices();
      this.Executor.Dispose();
    }
    #endregion
  }
}
