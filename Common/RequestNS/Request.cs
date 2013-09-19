using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public abstract class ARequest {
    #region Properties
    public string RelativeUrl { get; protected set; }
    public string RequestMethod { get; protected set; }
    public string ContentType { get; protected set; }
    public string Response { get; protected set; }
    #endregion

    #region Events
    public event EventHandler<RequestCompletedEventArgs> OnRequestCompleted;
    #endregion

    #region Members
    private IRequestExecutor _myExecutor = null;
    #endregion

    #region Constructor
    protected ARequest(IRequestExecutor executor) {
      this._myExecutor = executor;

    }
    #endregion

    #region Public methods
    public virtual void Execute() {
      this._myExecutor.Execute(this);
    }
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
