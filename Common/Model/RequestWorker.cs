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
    static AutoResetEvent autoEvent = new AutoResetEvent(false);
    #endregion

    #region Constructor
    public Worker(RequestExecutor theExecutor) {
      _theExecutor = theExecutor;
    }
    #endregion



    // This method will be called when the worker thread is started.
    public void DoWork() {
      //forever
      while (true) {
        Console.WriteLine("worker thread: wait for signal");
        autoEvent.WaitOne();
        Console.WriteLine("worker thread: signal was set; number of requests is: " + _theExecutor.numberOfPendingRequests());
        //assume at least one request is available: fetch the first one
        while ((_CurrentRequest = _theExecutor.getAndRemoveRequest()) != null) {
          //a request fetched; handle it
          Console.WriteLine("worker thread: handle the request: " + _CurrentRequest.RemoteUrl);
          WebRequest webRequest = HttpWebRequest.Create(_CurrentRequest.RemoteUrl);
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
          Console.WriteLine("worker thread: request is handled: " + _CurrentRequest.RemoteUrl);
          Console.WriteLine("worker thread: number of remaining requests is: " + _theExecutor.numberOfPendingRequests());
        }//endwhile
        //no more requests available
        Console.WriteLine("worker thread: no more requests available.");
      }
      Console.WriteLine("worker thread: terminating gracefully.");
    }


    public AutoResetEvent getEvent() {
      return autoEvent;
    }

  }
}
