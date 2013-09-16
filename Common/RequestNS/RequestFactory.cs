using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public class RequestFactory {
    #region Properties
    public IRequestExecutor Executor { get; private set; }
    #endregion

    public RequestFactory(IRequestExecutor service) {
      this.Executor = service;
    }

    public IRequest CreateWelcomeRequest() {
      return new RequestWelcome(this.Executor);
    }
  }
}
