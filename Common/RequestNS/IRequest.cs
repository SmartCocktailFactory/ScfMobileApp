using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestNS {
  public interface IRequest {
    #region Properties
    string RelativeUrl { get; }
    string RequestMethod { get; }
    string ContentType { get; }
    string Response { get; }
    #endregion

    #region Events
    event EventHandler<RequestCompletedEventArgs> OnRequestCompleted;
    #endregion

    #region Methods
    void Execute();
    #endregion
  }
}
