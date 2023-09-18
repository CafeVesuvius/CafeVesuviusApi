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
    }
}