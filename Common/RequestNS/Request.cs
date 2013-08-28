using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public abstract class ARequest : IRequest {
    #region IRequest
    public string RelativeUrl { get; protected set; }
    public string RequestMethod { get; protected set; }
    public string ContentType { get; protected set; }
    public string Response { get; protected set; }

    public event EventHandler<RequestCompletedEventArgs> OnRequestCompleted;

    public void Execute() {
      this._myExecutor.RunRequest(this);
    }
    #endregion

    #region Members
    private Service _myExecutor = null;
    #endregion

    #region Constructor
    protected ARequest(Service executor) {
      this._myExecutor = executor;

    }
    #endregion

    #region Public methods
    public void AddResponse(string response) {
      this.Response += response;
      this._NotifyExecuted();
    }
    #endregion

    #region Private methods
    private void _NotifyExecuted() {
      if (this.OnRequestCompleted != null) {
        Task.Factory.StartNew(() => {
          this.OnRequestCompleted(this, new RequestCompletedEventArgs(this));
        });
      }
    }
    #endregion
  }
}
