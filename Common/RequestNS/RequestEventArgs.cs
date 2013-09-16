using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestCompletedEventArgs : EventArgs {
    public ARequest Request { get; private set; }
    public RequestCompletedEventArgs(ARequest request) {
      this.Request = request;
    }
  }
}
