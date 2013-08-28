using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.RequestNS;

namespace Common {
  public class Service {
    #region Members
    HttpWebResponse _CurrentResponse = null;
    ARequest _CurrentRequest = null;
    #endregion

    #region Properties
    public string BaseUrl { get; private set; }
    public RequestFactory Factory { get; private set; }
    #endregion

    #region Constructor
    public Service(string baseUrl) {
      if (!baseUrl.StartsWith("http://")) {
        this.BaseUrl = "http://" + baseUrl;
      } else {
        this.BaseUrl = baseUrl;
      }
      this.Factory = new RequestFactory(this);
    }
    #endregion

    #region Public methods
    public void RunRequest(ARequest request) {
      this._CurrentRequest = request;
      WebRequest webRequest = HttpWebRequest.Create(this.BaseUrl + request.RelativeUrl);
      webRequest.ContentType = request.ContentType;
      webRequest.Method = request.RequestMethod;

      this._CurrentResponse = webRequest.GetResponse() as HttpWebResponse;

      if (this._CurrentResponse.StatusCode != HttpStatusCode.OK) {
        this._CurrentRequest.AddResponse("Invalid return status code: " + this._CurrentResponse.StatusCode);
      }
      StreamReader reader = new StreamReader(this._CurrentResponse.GetResponseStream());
      string content = reader.ReadToEnd();
      if (string.IsNullOrWhiteSpace(content)) {
        this._CurrentRequest.AddResponse("Response contained empty body...");
      } else {
        this._CurrentRequest.AddResponse("Response Body:\r\n" + content);
      }
    }
    #endregion

  }
}
