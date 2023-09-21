namespace CafeVesuviusApi.DTOs;

public class OrderDTO
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime? Created { get; set; }
    
    public bool Completed { get; set; }

    public virtual ICollection<OrderLineDTO> OrderLines { get; set; } = new List<OrderLineDTO>();
}