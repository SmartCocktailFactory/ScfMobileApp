using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  public class DrinkDetailViewModel : IViewModel {

    #region IViewModel
    public event EventHandler<ViewModelChangedEventArgs> OnViewModelChanged;

    public void DisposeViewModel() {
    }
    #endregion

  }
}
