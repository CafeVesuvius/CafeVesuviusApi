using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public partial class OrderLine
{
    public int Id { get; set; }

    public byte Quantity { get; set; }
    
    public long MenuItemId { get; set; }

    public long OrderId { get; set; }
}
