using System;
using System.Collections.Generic;

namespace pcstore_api.Models;

public partial class Computer
{
    public int ComputerId { get; set; }

    public int ProcessorId { get; set; }

    public int GraphicsCardId { get; set; }

    public int PsuId { get; set; }

    public int StorageId { get; set; }

    public int MemoryId { get; set; }

    public decimal? Weight { get; set; }

    public string? PortConfig { get; set; }

    public string? Description { get; set; }

    public virtual Memory? Memory { get; set; }

    public virtual Processor? Processor { get; set; }

    public virtual Psu? Psu { get; set; }

    public virtual Storage? Storage { get; set; }
    public virtual GraphicsCard? GraphicsCard { get; set; }
}
