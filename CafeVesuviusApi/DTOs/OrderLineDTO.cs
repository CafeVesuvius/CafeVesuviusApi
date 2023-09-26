using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.DTOs;

public class OrderLineDTO
{
    public int Id { get; set; }

    public byte Quantity { get; set; }
    
    public string Detail { get; set; } = null!;
    
    public virtual MenuItem MenuItem { get; set; } = null!;
}