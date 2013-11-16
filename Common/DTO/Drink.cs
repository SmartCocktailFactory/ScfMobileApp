using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO {
  public class Drink : ICloneable {
    #region Properties
    public string DrinkId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Recipe { get; set; }
    #endregion

    #region Constructor
    public Drink() {
      this.DrinkId = string.Empty;
      this.Name = string.Empty;
      this.Description = string.Empty;
      this.Recipe = string.Empty;
    }

    #endregion

    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}