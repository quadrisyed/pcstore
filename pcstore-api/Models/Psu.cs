using System;
using System.Collections.Generic;

namespace pcstore_api.Models;
public partial class Psu
{
    public int PsuId { get; set; }

    public int? Watts { get; set; }

    public string? Unit { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    public override string ToString()
    {
        return $"{this.Watts} {this.Unit}";
    }
}
