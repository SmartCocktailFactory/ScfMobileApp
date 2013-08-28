using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestDrinkList : ARequest {

    #region Constructor
    public RequestDrinkList(Service executor)
      : base(executor) {
      this.RelativeUrl = "/cocktails";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

  }
}
