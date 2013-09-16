using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel {
  class DrinksChangedEventArgs : EventArgs {
    public List<Drink> Drinks { get; private set; }

    public DrinksChangedEventArgs(IList<Drink> drinks) {
      this.Drinks = drinks.ToList();
    }
  }
}
