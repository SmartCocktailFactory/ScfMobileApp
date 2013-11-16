using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.RequestNS;

namespace Common.Model {


  public class RequestExecutor : RequestNS.IRequestExecutor {
    #region Members
    List<ARequest> _requestQueue = new List<ARequest>();
    private object _requestLock = new object();
    private Worker _WorkerThread;
    #endregion


    #region Constructor
    public RequestExecutor() {
      _WorkerThread = new Worker(this);
      Thread workerThread = new Thread(_WorkerThread.DoWork);
      workerThread.Start();
    }
    #endregion

    #region Public methods
    public void Execute(ARequest request) {
      //Queue the request
      lock (this._requestLock) {
        _requestQueue.Add(request);
      }
      this._WorkerThread.TriggerWorker();
    }

    public int numberOfPendingRequests() {
      return _requestQueue.Count;
    }

    public ARequest getAndRemoveRequest() {
      ARequest tempReq = null;
      lock (this._requestLock) {
        if (_requestQueue.Count > 0) {
          tempReq = _requestQueue[0];
          _requestQueue.Remove(tempReq);
        }
      }

      return tempReq;
    }

    public void Dispose() {
      this._WorkerThread.Stop();
    }
    #endregion
  }
}
