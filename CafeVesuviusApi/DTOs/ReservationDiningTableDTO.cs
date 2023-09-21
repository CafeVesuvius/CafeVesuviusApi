using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.DTOs;

public class ReservationDiningTableDTO
{
    public long Id { get; set; }

    public DiningTable DiningTable { get; set; } = null!;
}