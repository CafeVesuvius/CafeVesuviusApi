using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class DiningTable
{
    public long Id { get; set; }
    
    public string Number { get; set; } = null!;

    public byte Seats { get; set; }
}
