using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.DTOs;

public class OrderLineDTO
{
    public int Id { get; set; }

    public byte Quantity { get; set; }
    
    public virtual MenuItem MenuItem { get; set; } = null!;
}