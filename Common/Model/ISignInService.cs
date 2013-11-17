using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  interface ISignInService {
    #region Properties
    string WelcomeMessage { get; }
    #endregion

    #region Events
    event EventHandler<WelcomeMessageReceivedEventArgs> OnWelcomeMessageChanged;
    #endregion
  }
}
