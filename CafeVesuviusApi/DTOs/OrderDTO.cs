namespace CafeVesuviusApi.DTOs;

public class OrderDTO
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public bool Completed { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<OrderLineDTO> OrderLines { get; set; } = new List<OrderLineDTO>();
}