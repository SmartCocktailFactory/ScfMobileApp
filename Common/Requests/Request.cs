using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
  public abstract class Request {
    #region Properties
    public string RelativeUrl { get; protected set; }
    public string RequestMethod { get; protected set; }
    public string ContentType { get; protected set; }
    public string Response { get; protected set; }
    #endregion

    #region Public methods
    public void AddResponse(string response) {
      this.Response += response;
    }
    #endregion

  }
}
