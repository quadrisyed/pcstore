using System;
using System.Collections.Generic;

namespace pcstore_api.Models;

public partial class Processor
{
    public int ProcessorId { get; set; }

    public string? Model { get; set; }

    public int? Cores { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
    public override string ToString()
    {
        return $"{this.Brand?.Name} {this.Model} {this.Cores}";
    }
}
