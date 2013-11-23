using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class OrderDetails {

    #region Properties
    public string DrinkName { get; set; }
    public string OrderId { get; set; }
    public string OrderStatus { get; set; }
    public Common.DTO.StateId OrderStateId { get; set; }
    public string SecondsToFinish { get; set; }
    #endregion

    #region Constructor
    public OrderDetails(Common.DTO.Drink drink, Common.DTO.Order order) {
      this.DrinkName = drink.Name;
      this.OrderId = order.OrderId;
      this.OrderStatus = order.OrderStatus;
      this.OrderStateId = order.OrderStateId;
      this.SecondsToFinish = SecondsToFinish;
    }
    #endregion
  }
}
