using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class OrderChangedEventArgs : EventArgs {
    public Order Order { get; private set; }

    public OrderChangedEventArgs(Order order) {
      this.Order = order;
    }
  }
}
