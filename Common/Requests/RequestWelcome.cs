using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
  public class RequestWelcome : Request {

    #region Constructor
    public RequestWelcome() {
      this.RelativeUrl = "";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";         
    }
    #endregion

  }
}
