using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.DTOs;

public class ReservationDiningTableDTO
{
    public int Id { get; set; }

    public DiningTable DiningTable { get; set; } = null!;
}