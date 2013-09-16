using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestWelcome : ARequest {

    #region Constructor
    public RequestWelcome(IRequestExecutor executor)
      : base(executor) {
      this.RelativeUrl = "";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";         
    }
    #endregion

  }
}
