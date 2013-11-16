using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO {

  public enum StateId {
    Pending,
    InProgress,
    Completed,
    Failed
  };

  public class Order : ICloneable {
    public string OrderId { get; set; }
    public string DrinkId { get; set; }
    public string OrderStatus { get; set; }
    public StateId OrderStateId { get; set; }
    public int ExpectedSecondsToDeliver { get; set; }

    public Order() {
      this.OrderId = string.Empty;
      this.DrinkId = string.Empty;
      this.OrderStatus = string.Empty;
      this.OrderStateId = StateId.InProgress;
      this.ExpectedSecondsToDeliver = 60;
    }

    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}
