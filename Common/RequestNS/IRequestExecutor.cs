using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public interface IRequestExecutor {
    void Execute(ARequest request);
  }
}
