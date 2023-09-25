using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.Services
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationDTO>> GetReservations();
        Task<Reservation> GetReservation(int id);
        Task<Reservation> PostReservation(Reservation reservation);
        Task<bool> PutReservation(int id, Reservation reservation);
        Task<bool> DeleteReservation(int id);
        Task<IEnumerable<DiningTable>> GetDiningTables();
        Task<IEnumerable<DiningTable>> GetAvailableDiningTables(DateTime reservationTime);
        Task<DiningTable> PostDiningTable(DiningTable diningTable);
        Task<bool> PutDiningTable(int id, DiningTable diningTable);
        Task<bool> DeleteDiningTable(int id);
    }
}