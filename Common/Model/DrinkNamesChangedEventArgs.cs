
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model {
  class DrinkNamesChangedEventArgs : EventArgs {
    public List<string> DrinkNames { get; private set; }

    public DrinkNamesChangedEventArgs(IList<string> drinkNames) {
      this.DrinkNames = drinkNames.ToList();
    }
  }
}
