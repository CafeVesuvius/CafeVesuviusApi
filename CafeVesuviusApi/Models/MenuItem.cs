using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class MenuItem
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public bool Active { get; set; }

    public long MenuId { get; set; }
    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}
