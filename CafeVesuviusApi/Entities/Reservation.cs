using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Entities;

public class Reservation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime Time { get; set; }
    
    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<ReservationDiningTable> ReservationDiningTables { get; set; } = new List<ReservationDiningTable>();
}
