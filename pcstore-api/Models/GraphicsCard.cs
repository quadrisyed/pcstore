using System;
using System.Collections.Generic;

namespace pcstore_api.Models;

public partial class GraphicsCard
{
    public int GraphicCardId { get; set; }

    public string? Model { get; set; }

    public int? MemoryId { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Memory? Memory { get; set; }
    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();

    
    public override string ToString()
    {
        return $"{this.Model} {this.Memory?.Capacity} {this.Memory?.Unit}";
    }
}
