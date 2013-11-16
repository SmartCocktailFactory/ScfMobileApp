using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Common.RequestNS {
  class RequestOrderStatus : ARequest {

    #region Members
    private string _OrderId = string.Empty;
    #endregion

    #region Properties
    public string OrderId {
      get {
        return this._OrderId;
      }
      set {
        this._SetOrderId(value);
      }
    }
    #endregion

    #region Constructor
    public RequestOrderStatus(string baseUrl, IRequestExecutor executor)
      : base(baseUrl, executor) {
      this.RelativeUrl = "/orders/{0}";
      this.ContentType = "application/json";
      this.RequestMethod = "GET";
    }
    #endregion

    #region Public method
		public DTO.Order GetOrder() {
			DTO.Order order = new DTO.Order();
      var rawOrder = JObject.Parse(this.Response);
      int seconds = 0;

      order.DrinkId = rawOrder["drink_id"].ToString();
      order.OrderStatus = rawOrder["status"].ToString();

      int.TryParse(rawOrder["expected_time_to_completion"].ToString(), out seconds);
      order.ExpectedSecondsToDeliver = seconds;

      return order;
    }
    #endregion

    #region Private methods
    private void _SetOrderId(string orderId) {
      this._OrderId = orderId;
      this.RelativeUrl = string.Format(this.RelativeUrl, orderId);
    }
    #endregion

  }
}