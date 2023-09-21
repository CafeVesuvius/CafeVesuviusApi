﻿using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class OrderLine
{
    public long Id { get; set; }

    public byte Quantity { get; set; }
    
    public long MenuItemId { get; set; }

    public long OrderId { get; set; }
}