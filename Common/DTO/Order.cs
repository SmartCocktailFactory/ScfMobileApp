using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO {

  public class Order {
    public string OrderId { get; set; }
    public string DrinkId { get; set; }
    public string OrderStatus { get; set; }
    public int ExpectedSecondsToDeliver { get; set; }

    public Order() {
      this.OrderId = string.Empty;
      this.DrinkId = string.Empty;
      this.OrderStatus = string.Empty;
      this.ExpectedSecondsToDeliver = 60;
    }
  }
}
