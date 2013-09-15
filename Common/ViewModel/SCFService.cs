using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ViewModel {
  class SCFService : IScfService {

    #region Members
    private Common.Service _Service = null;
    private string _WelcomeMessage = string.Empty;
    private List<Drink> _DrinkList = new List<Drink>();
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
    #endregion

    #region IScfService
    public event EventHandler OnWelcomeMessageChanged;
    public event EventHandler OnDrinksChanged;

    event EventHandler IScfService.OnWelcomeMessageChanged {
      add { this.OnWelcomeMessageChanged += value; }
      remove { this.OnWelcomeMessageChanged -= value; }
    }

    event EventHandler IScfService.OnDrinksChanged {
      add { this.OnDrinksChanged += value; }
      remove { this.OnDrinksChanged -= value; }
    }

    string IScfService.WelcomeMessage {
      get {
        return this._WelcomeMessage;
      }
    }

    IList<string> IScfService.DrinkNames {
      get {
        return this._DrinkList.Select(x => x.Name).ToList();
      }
    }

    void IScfService.RequestWelcomeMessage() {
      Common.RequestNS.IRequest request = this._Service.Factory.CreateWelcomeRequest();
      request.OnRequestCompleted += WelcomeRequest_OnRequestCompleted;
      request.Execute();
    }

    void IScfService.RequestDrinkList() {
      throw new NotImplementedException();
    }

    Drink IScfService.GetDrinkDetails(string drinkName) {
      throw new NotImplementedException();
    }

    void IScfService.Order(string drinkName) {
      throw new NotImplementedException();
    }
    #endregion

    #region Private methods
    private void _CreateRemote(string remoteBaseUrl) {
      this._Service = new Common.Service(remoteBaseUrl);
    }

    private void WelcomeRequest_OnRequestCompleted(object sender, Common.RequestNS.RequestCompletedEventArgs e) {
      this._WelcomeMessage = e.Request.Response;

      if (this.OnWelcomeMessageChanged != null) {
        this.OnWelcomeMessageChanged(this, new EventArgs());
      }
    }
    #endregion
  }
}
