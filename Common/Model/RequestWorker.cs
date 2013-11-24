﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.RequestNS;

namespace Common.Model {


  //worker thread
  public class Worker {
    #region Members
    private HttpWebResponse _CurrentResponse = null;
    private ARequest _CurrentRequest = null;
    private RequestExecutor _theExecutor;
    private AutoResetEvent _TriggerWorker = new AutoResetEvent(false);
    private bool _IsWorking = true;
    #endregion

    #region Properties
    public bool Working {
      get {
        lock (this) {
          return this._IsWorking;
        }
      }
      private set {
        lock(this) {
          this._IsWorking = value;
        }
      }
    }
    #endregion

    #region Constructor
    public Worker(RequestExecutor theExecutor) {
      _theExecutor = theExecutor;
      this.Working = true;
    }
    #endregion

    #region Public methods
    // This method will be called when the worker thread is started.
    public void DoWork() {
      //forever
      while (this.Working) {
        Console.WriteLine("worker thread: wait for signal");
        _TriggerWorker.WaitOne();
        Console.WriteLine("worker thread: signal was set; number of requests is: " + _theExecutor.numberOfPendingRequests());
        //assume at least one request is available: fetch the first one
        while ((_CurrentRequest = _theExecutor.getAndRemoveRequest()) != null) {
          //a request fetched; handle it
          Console.WriteLine("worker thread: handle the request: " + _CurrentRequest.RemoteUrl);
          WebRequest webRequest = HttpWebRequest.Create(_CurrentRequest.RemoteUrl);
          webRequest.ContentType = _CurrentRequest.ContentType;
          webRequest.Method = _CurrentRequest.RequestMethod;
          webRequest.Timeout = 5000;

          this._CurrentResponse = null;
          try {
            this._CurrentResponse = webRequest.GetResponse() as HttpWebResponse;
          } catch (WebException ex) {
            this._CurrentRequest.SetRequestFailed("Web request failed");

          }

          if (this._CurrentResponse != null) {
            if (this._CurrentResponse.StatusCode != HttpStatusCode.OK) {
              this._CurrentRequest.SetRequestFailed("Invalid return status code: " + this._CurrentResponse.StatusCode);
            }
            StreamReader reader = new StreamReader(this._CurrentResponse.GetResponseStream());
            string content = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(content)) {
              this._CurrentRequest.AddResponse("Response contained empty body...");
            } else {
              this._CurrentRequest.AddResponse(content);
            }
          }
        }
      }
    }

    public void TriggerWorker() {
      this._TriggerWorker.Set();  
    }

    public void Stop() {
      this.Working = false;
      this.TriggerWorker();
    }
    #endregion
  }
}
