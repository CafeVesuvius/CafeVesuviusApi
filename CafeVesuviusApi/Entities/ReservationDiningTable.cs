using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class ReservationDiningTable
{
    public long Id { get; set; }

    public long ReservationId { get; set; }

    public long DiningTableId { get; set; }
}
