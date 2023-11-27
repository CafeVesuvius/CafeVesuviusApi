using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationDTO>> GetReservations();
        Task<Reservation> GetReservation(int id);
        Task<ReservationResponse> PostReservation(ReservationRequest reservationRequest);
        Task<bool> PutReservation(int id, Reservation reservation);
        Task<bool> DeleteReservation(int id);
        Task<IEnumerable<DiningTable>> GetDiningTables();
        Task<IEnumerable<DiningTable>> GetAvailableDiningTables(DateTime reservationTime);
        Task<DiningTable> PostDiningTable(DiningTable diningTable);
        Task<bool> PutDiningTable(int id, DiningTable diningTable);
        Task<bool> DeleteDiningTable(int id);
        Task<AvailableResponse> IsAvailableByDateTime(DateTime reservationTime);
        Task<AvailableResponse> IsAvailableByDateOnly(DateOnly reservationDate);
    }
}