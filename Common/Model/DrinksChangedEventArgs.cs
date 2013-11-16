using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class DrinksChangedEventArgs : EventArgs {
		public List<DTO.Drink> Drinks { get; private set; }

		public DrinksChangedEventArgs(IList<DTO.Drink> drinks) {
      this.Drinks = drinks.ToList();
    }
  }
}
