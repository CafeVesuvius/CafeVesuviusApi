using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class ReservationDiningTable
{
    public int Id { get; set; }

    public long ReservationID { get; set; }

    public long DiningTableID { get; set; }
}
