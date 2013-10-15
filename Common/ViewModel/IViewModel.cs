using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  interface IViewModel {
    #region Events
    event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;
    #endregion

    #region Methods
    void DisposeViewModel();
    #endregion
  }
}
