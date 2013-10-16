﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public abstract class ARequest {
    #region Properties
    public string BaseUrl { get; private set; }
    public string RelativeUrl { get; protected set; }
    public string RemoteUrl {
      get {
        return this.BaseUrl + this.RelativeUrl;
      }
    }
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
    protected ARequest(string baseUrl, IRequestExecutor executor) {
      this._myExecutor = executor;
      this._SetBaseUrl(baseUrl);
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

    private void _SetBaseUrl(string url) {
      if (!url.StartsWith("http://")) {
        this.BaseUrl = "http://" + url;
      } else {
        this.BaseUrl = url;
      }
    }
    #endregion
  }
}
