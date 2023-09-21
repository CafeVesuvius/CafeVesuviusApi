using System;
using System.Collections.Generic;

namespace CafeVesuviusApi.Models;

public partial class Reservation
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime Time { get; set; }
    
    public DateTime? Created { get; set; }

    public virtual ICollection<ReservationDiningTable> ReservationDiningTables { get; set; } = new List<ReservationDiningTable>();
}
