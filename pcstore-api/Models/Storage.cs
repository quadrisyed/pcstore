using System;
using System.Collections.Generic;

namespace pcstore_api.Models;
public partial class Storage
{
    public int StorageId { get; set; }

    public string? Type { get; set; }

    public int? Capacity { get; set; }

    public string? Unit { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
    public override string ToString()
    {
        return $"{Brand?.Name} {this.Capacity} {this.Unit} {this.Type}";
    }
}
