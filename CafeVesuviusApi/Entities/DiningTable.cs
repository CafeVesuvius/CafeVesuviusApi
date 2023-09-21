using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public partial class DiningTable
{
    public int Id { get; set; }
    
    public string Number { get; set; } = null!;

    public byte Seats { get; set; }
}
