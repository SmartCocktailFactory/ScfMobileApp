using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ViewModel {
  class SCFServiceResponseEventArgs : EventArgs {
    public string Response { get; private set; }
    public bool Successful { get; private set; }
    public SCFServiceResponseEventArgs(string response, bool successful) {
      this.Response = response;
      this.Successful = successful;
    }
  }
}
