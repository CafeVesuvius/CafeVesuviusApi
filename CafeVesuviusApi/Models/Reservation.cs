using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class Reservation
{
    public long Id { get; set; }

    public DateTime FromTime { get; set; }

    public DateTime ToTime { get; set; }

    public string ReservationName { get; set; } = null!;

    public virtual ICollection<ReservationDiningTable> ReservationDiningTables { get; set; } = new List<ReservationDiningTable>();
}
