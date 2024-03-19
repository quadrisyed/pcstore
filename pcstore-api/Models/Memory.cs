using System;
using System.Collections.Generic;

namespace pcstore_api.Models;

public partial class Memory
{
    public int MemoryId { get; set; }

    public string? Type { get; set; }

    public int? Capacity { get; set; }

    public string? Unit { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    public virtual ICollection<GraphicsCard> GraphicsCards { get; set; } = new List<GraphicsCard>();

    public override string ToString()
    {
        return $"{this.Capacity} {this.Unit} {this.Type}";
    }
}
