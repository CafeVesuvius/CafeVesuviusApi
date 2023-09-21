using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class ReservationDiningTable
{
    public int Id { get; set; }

    public long ReservationId { get; set; }

    public long DiningTableId { get; set; }
}
