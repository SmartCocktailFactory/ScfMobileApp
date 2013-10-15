using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Model {
  class ScfSignInService : ISignInService {

    #region Members
    private RequestExecutor _Executor = null;
    private RequestNS.RequestFactory _Factory = null;
    private string _WelcomeMessage = string.Empty;
    #endregion

    #region Model.ISignInService

    public event EventHandler<WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;
    
    public string WelcomeMessage
    {
        get
        {
            if (string.IsNullOrEmpty(this._WelcomeMessage))
                {
                this._RequestWelcomeMessage();
            }
            return this._WelcomeMessage;
        }
    }

    public string ScfRemoteUrl
    {
        get
        {
            if (this._Executor == null)
            {
                return string.Empty;
            }
            return this._Executor.BaseUrl;
        }
        set
        {
            this._SetRemoteUrl(value);
        }
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

      private void _RequestWelcomeMessage()
    {
        Task.Factory.StartNew(() =>
        {
            RequestNS.ARequest request = this._Factory.CreateWelcomeRequest();
            request.OnRequestCompleted += welcomeRequest_OnRequestCompleted;
            request.Execute();
        });
    }

    private void _SetRemoteUrl(string remoteUrl)
    {
        if (!string.Equals(this.ScfRemoteUrl, remoteUrl))
        {
            this._Executor = new RequestExecutor(remoteUrl);
            this._Factory = new RequestNS.RequestFactory(this._Executor);
        }
    }

    #endregion

    #region Event handlers
    void welcomeRequest_OnRequestCompleted(object sender, RequestNS.RequestCompletedEventArgs e)
    {
        RequestNS.RequestWelcome welcomeMessage = e.Request as RequestNS.RequestWelcome;

        this._WelcomeMessage = welcomeMessage.Response;
        this._NotifyWelcomeMessageChanged();
    }
    #endregion
  }
}
