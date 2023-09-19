using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation> GetReservation(long id);
        Task<Reservation> PostReservation(Reservation reservation);
        Task<bool> PutReservation(long id, Reservation reservation);
        Task<bool> DeleteReservation(long id);
        Task<IEnumerable<DiningTable>> GetDiningTables();
        Task<IEnumerable<DiningTable>> GetAvailableDiningTables(DateTime reservationTime);
        Task<DiningTable> PostDiningTable(DiningTable diningTable);
        Task<bool> PutDiningTable(long id, DiningTable diningTable);
        Task<bool> DeleteDiningTable(long id);
    }
}