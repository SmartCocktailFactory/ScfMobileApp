using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class ScfService : ViewModel.ICocktailFactory {

    #region Members
    private RequestExecutor _Executor = null;
    private RequestNS.RequestFactory _Factory = null;
    private string _WelcomeMessage = string.Empty;
    List<ViewModel.Drink> _Drinks = new List<ViewModel.Drink>();
    #endregion

    #region Properties
    public IList<ViewModel.Drink> Drinks {
      get {
        if (this._Drinks.Count == 0) {
          this._RequestDrinks();
        }
        return this._Drinks.ToList();
      }
    }
    #endregion

    #region ViewModel.ICocktailFactory
    public event EventHandler<ViewModel.WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;
    public event EventHandler<ViewModel.DrinkNamesChangedEventArgs> OnDrinkNamesChanged;
    public event EventHandler<ViewModel.DrinksChangedEventArgs> OnDrinksChanged;
    public event EventHandler<ViewModel.OrderChangedEventArgs> OnOrderChanged;

    public string WelcomeMessage {
      get {
        if (string.IsNullOrEmpty(this._WelcomeMessage)) {
          this._RequestWelcomeMessage();
        }
        return this._WelcomeMessage;
      }
    }

    public IList<string> DrinkNames {
      get { return this.Drinks.Select(x => x.Name).ToList(); }
    }

    public ViewModel.Order CurrentOrder {
      get { throw new NotImplementedException(); }
    }

    public string ScfRemoteUrl {
      get {
        if (this._Executor == null) {
          return string.Empty;
        }
        return this._Executor.BaseUrl;
      }
      set {
        this._SetRemoteUrl(value);
      }
    }
    #endregion

    #region Private methods
    private void _RequestWelcomeMessage() {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateWelcomeRequest();
        request.OnRequestCompleted += welcomeRequest_OnRequestCompleted;
        request.Execute();
      });
    }

    private void _RequestDrinks() {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateGetDrinkRequest();
        request.OnRequestCompleted += getDrinkRequest_OnRequestCompleted;
        request.Execute();
      });
    }

    private void _SetRemoteUrl(string remoteUrl) {
      if (!string.Equals(this.ScfRemoteUrl, remoteUrl)) {
        this._Executor = new RequestExecutor(remoteUrl);
        this._Factory = new RequestNS.RequestFactory(this._Executor);
      }
    }

    private void _NotifyWelcomeMessageChanged() {
      if (this.OnWelcomeMessageChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnWelcomeMessageChanged(this, new ViewModel.WelcomeMessageReceivedEventArgs(this._WelcomeMessage));
        });
      }
    }

    private void _NotifyDrinkListChanged() {
      if (this.OnDrinkNamesChanged != null) {
        Task.Factory.StartNew(() => {
          this.OnDrinkNamesChanged(this, new ViewModel.DrinkNamesChangedEventArgs(this.DrinkNames));
        });
      }
    }

    #endregion

    #region Event handlers
    void welcomeRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestWelcome welcomeMessage = e.Request as RequestNS.RequestWelcome;

      this._WelcomeMessage = welcomeMessage.Response;
      this._NotifyWelcomeMessageChanged();
    }

    void getDrinkRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      RequestNS.RequestDrinkList drinkList = e.Request as RequestNS.RequestDrinkList;
      this._Drinks = drinkList.GetDrinks();

      this._NotifyDrinkListChanged();
    }
    #endregion
  }
}
