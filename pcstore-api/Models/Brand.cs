using System;
using System.Collections.Generic;

namespace pcstore_api.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GraphicsCard> GraphicsCards { get; set; } = new List<GraphicsCard>();

    public virtual ICollection<Memory> Memories { get; set; } = new List<Memory>();

    public virtual ICollection<Processor> Processors { get; set; } = new List<Processor>();

    public virtual ICollection<Psu> Psus { get; set; } = new List<Psu>();

    public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
}
