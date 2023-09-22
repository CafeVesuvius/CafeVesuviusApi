using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class OrderLine
{
    public int Id { get; set; }

    public byte Quantity { get; set; }
    
    public int MenuItemID { get; set; }

    public int OrderID { get; set; }
}
