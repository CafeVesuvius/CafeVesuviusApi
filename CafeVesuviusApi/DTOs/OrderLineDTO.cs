using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.DTOs;

public class OrderLineDTO
{
    public long Id { get; set; }

    public byte Quantity { get; set; }
    
    public virtual MenuItem MenuItem { get; set; } = null!;
}