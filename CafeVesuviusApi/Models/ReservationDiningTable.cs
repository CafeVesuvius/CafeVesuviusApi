using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class ReservationDiningTable
{
    public long Id { get; set; }

    public long ReservationId { get; set; }

    public long DiningTableId { get; set; }

    public virtual DiningTable DiningTable { get; set; } = null!;

    public virtual Reservation Reservation { get; set; } = null!;
}
