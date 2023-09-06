using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class DiningTable
{
    public long Id { get; set; }

    public byte Seats { get; set; }

    public virtual ICollection<ReservationDiningTable> ReservationDiningTables { get; set; } = new List<ReservationDiningTable>();
}
