using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.DTOs;

public class ReservationDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public byte People { get; set; }

    public DateTime Time { get; set; }
    
    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<DiningTable> ReservationDiningTables { get; set; } = new List<DiningTable>();
}