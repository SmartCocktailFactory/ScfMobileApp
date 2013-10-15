using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class OrderChangedEventArgs : EventArgs {
    public Common.ViewModel.Order Order { get; private set; }

    public OrderChangedEventArgs(Common.ViewModel.Order order) {
      this.Order = order;
    }
  }
}
