﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class Order {
    public string OrderId { get; set; }
    public string DrinkName { get; set; }
    public TimeSpan ExpectedTimeToDeliver { get; set; }
  }
}