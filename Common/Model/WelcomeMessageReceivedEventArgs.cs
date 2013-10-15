using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class WelcomeMessageReceivedEventArgs {
    public string WelcomeMessage { get; private set; }

    public WelcomeMessageReceivedEventArgs(string message) {
      this.WelcomeMessage = message;
    }
  }
}
