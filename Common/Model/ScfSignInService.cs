using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Model {
  class ScfSignInService : ISignInService {

    #region Members
    private RequestNS.RequestFactory _Factory = null;
    private string _WelcomeMessage = string.Empty;
    #endregion

    #region Model.ISignInService

    public event EventHandler<WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;

    public string WelcomeMessage {
      get {
        this._RequestWelcomeMessage();
        return this._WelcomeMessage;
      }
    }
    #endregion

    #region Constructor
    public ScfSignInService(RequestNS.RequestFactory requestFactory) {
      this._Factory = requestFactory;
    }
    #endregion

    #region Public methods
    public void ResetService() {
      this._WelcomeMessage = string.Empty;
    }
    #endregion

    #region Private methods

    private void _NotifyWelcomeMessageChanged()
    {
        if (this.OnWelcomeMessageChanged != null)
        {
            Task.Factory.StartNew(() =>
            {
                this.OnWelcomeMessageChanged(this, new WelcomeMessageReceivedEventArgs(this._WelcomeMessage));
            });
        }
    }

    private void _RequestWelcomeMessage() {
      Task.Factory.StartNew(() => {
        RequestNS.ARequest request = this._Factory.CreateWelcomeRequest();
        request.OnRequestCompleted += welcomeRequest_OnRequestCompleted;
        request.Execute();
      });
    }

    #endregion

    #region Event handlers
    void welcomeRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e) {
      if (e.Request.State != RequestNS.RequestStates.Successful) {
        return;
      }
      RequestNS.RequestWelcome welcomeMessage = e.Request as RequestNS.RequestWelcome;

      this._WelcomeMessage = welcomeMessage.Response;
      this._NotifyWelcomeMessageChanged();
    }
    #endregion
  }
}
