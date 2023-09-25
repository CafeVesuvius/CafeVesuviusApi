using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class ReservationDiningTable
{
    public int Id { get; set; }

    public int ReservationID { get; set; }

    public int DiningTableID { get; set; }
}
