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
    private HttpWebResponse _CurrentResponse = null;
    List<ARequest> _requestQueue = new List<ARequest>();
    private object _requestLock = new object();
    #endregion


    #region Constructor
    public RequestExecutor() {
      Worker workerObject = new Worker(this);
      Thread workerThread = new Thread(workerObject.DoWork);
      workerThread.Start();
    }
    #endregion

    #region Public methods
    public void Execute(ARequest request) {
      //Queue the request
      lock (this._requestLock) {
        _requestQueue.Add(request);
      }
      ///signal...
      //end of queueing
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
    #endregion

  }

  //worker thread (taken from msdn)
  public class Worker {
    #region Members
    private HttpWebResponse _CurrentResponse = null;
    private ARequest _CurrentRequest = null;
    private RequestExecutor _theExecutor;
    #endregion

    #region Constructor
    public Worker(RequestExecutor theExecutor) {
      _theExecutor = theExecutor;
    }
    #endregion



    // This method will be called when the thread is started.
    public void DoWork() {
      _shouldStop = false;
      while (!_shouldStop) {
        //wait for signal
        //TODO
        //assume at least one request is available: fetch it
        //if possible, take a request from the list
        while ((_CurrentRequest=_theExecutor.getAndRemoveRequest()) == null) {
          //no request available; delay
          Thread.Sleep(1000);
        }
        //a request found; handle it
        Console.WriteLine("worker thread: fetched a request: "+ _CurrentRequest.RemoteUrl);
        WebRequest webRequest = HttpWebRequest.Create( _CurrentRequest.RemoteUrl);
        webRequest.ContentType = _CurrentRequest.ContentType;
        webRequest.Method = _CurrentRequest.RequestMethod;

        this._CurrentResponse = webRequest.GetResponse() as HttpWebResponse;

        if (this._CurrentResponse.StatusCode != HttpStatusCode.OK) {
          this._CurrentRequest.AddResponse("Invalid return status code: " + this._CurrentResponse.StatusCode);
        }
        StreamReader reader = new StreamReader(this._CurrentResponse.GetResponseStream());
        string content = reader.ReadToEnd();
        if (string.IsNullOrWhiteSpace(content)) {
          this._CurrentRequest.AddResponse("Response contained empty body...");
        } else {
          this._CurrentRequest.AddResponse(content);
        }
      }
      Console.WriteLine("worker thread: terminating gracefully.");
    }
    public void RequestStop() {
      _shouldStop = true;
    }




    // Volatile is used as hint to the compiler that this data
    // member will be accessed by multiple threads.
    private volatile bool _shouldStop;
  }
}
