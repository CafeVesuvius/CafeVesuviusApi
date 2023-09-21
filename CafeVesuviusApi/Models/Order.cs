using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class Order
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime? Created { get; set; }
    
    public bool Completed { get; set; }

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}
