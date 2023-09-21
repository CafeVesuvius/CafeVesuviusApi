namespace CafeVesuviusApi.DTOs;

public class ReservationDTO
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime Time { get; set; }
    
    public DateTime? Created { get; set; }

    public virtual ICollection<ReservationDiningTableDTO> ReservationDiningTables { get; set; } = new List<ReservationDiningTableDTO>();
}