using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
  public class RequestDrinkList : Request {

    #region Constructor
    public RequestDrinkList() {
      this.RelativeUrl = "/cocktails";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

  }
}
