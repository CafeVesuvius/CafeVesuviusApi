namespace CafeVesuviusApi.DTOs;

public class OrderDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime? CreatedDate { get; set; }
    
    public bool IsCompleted { get; set; }

    public virtual ICollection<OrderLineDTO> OrderLines { get; set; } = new List<OrderLineDTO>();
}