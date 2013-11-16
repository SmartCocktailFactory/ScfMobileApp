using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  public class OrderChangedEventArgs : EventArgs {
		public Common.DTO.Order Order { get; private set; }

		public OrderChangedEventArgs(Common.DTO.Order order) {
      this.Order = order;
    }
  }
}
