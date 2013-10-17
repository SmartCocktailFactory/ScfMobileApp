using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class Order {
    public string OrderId { get; set; }
    public string DrinkId { get; set; }
    public string OrderStatus { get; set; }
    public int ExpectedSecondsToDeliver { get; set; }
  }
}
