using CafeVesuviusApi.Models;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Services.Utilities;

namespace CafeVesuviusApi.Services;

public class ReservationRepository : IReservationRepository
{
    private readonly CafeVesuviusContext _context;

    public ReservationRepository(CafeVesuviusContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Reservation>> GetReservations()
    {
        List<Reservation>? reservations = await _context.Reservations.ToListAsync();
        if (reservations.Count > 0)
        {
            foreach (Reservation reservation in reservations)
            {
                reservation.ReservationDiningTables = await _context.ReservationDiningTables.Where(reservationDiningTable => reservationDiningTable.ReservationId == reservation.Id).ToListAsync();
            }
        }
        return reservations;
    }
    
    public async Task<IEnumerable<Reservation>> GetReservationsByDateTime(DateTime from, DateTime? to)
    {
        List<Reservation>? reservations = await _context.Reservations.Where(reservation =>
            reservation.Time.Date >= from.Date && reservation.Time.Date <= ((to.HasValue) ? to.Value.Date : from.Date)).ToListAsync();
        
        if (reservations.Count > 0)
        {
            foreach (Reservation reservation in reservations)
            {
                reservation.ReservationDiningTables = await _context.ReservationDiningTables.Where(reservationDiningTable => reservationDiningTable.ReservationId == reservation.Id).ToListAsync();
            }
        }
        return reservations;
    }
    
    public async Task<Reservation> GetReservation(long id)
    {
        Reservation? reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return reservation;
        }
        reservation.ReservationDiningTables = await _context.ReservationDiningTables.Where(reservationDiningTable => reservationDiningTable.ReservationId == reservation.Id).ToListAsync();

        return reservation;
    }
    
    public async Task<Reservation?> PostReservation(Reservation reservation)
    {
        DiningTable? reservationTable = GetAvailableTable(reservation.Time);

        if (reservationTable != null)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            ReservationDiningTable reservationDiningTable = new();
            reservationDiningTable.ReservationId = reservation.Id;
            reservationDiningTable.DiningTableId = reservationTable.Id;
            
            reservation.ReservationDiningTables.Add(reservationDiningTable);
        }
        else return null;
        return reservation;
    }
    
    public async Task<bool> PutReservation(long id, Reservation reservation)
    {
        _context.Entry(reservation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault())
            {
                return false;
            }
        }
        return true;
    }
    
    public async Task<bool> DeleteReservation(long id)
    {
        Reservation? reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null) return false;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<DiningTable> PostDiningTable(DiningTable diningTable)
    {
        _context.DiningTables.Add(diningTable);
        await _context.SaveChangesAsync();

        return diningTable;
    }
    
    public async Task<bool> DeleteDiningTable(long id)
    {
        DiningTable? diningTable = await _context.DiningTables.FindAsync(id);

        if (diningTable == null) return false;

        _context.DiningTables.Remove(diningTable);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<IEnumerable<DiningTable>> GetDiningTables()
    {
        List<DiningTable>? tables = await _context.DiningTables.ToListAsync();
        return tables;
    }
    
    public DiningTable? GetAvailableTable(DateTime reservationTime)
    {
        List<DiningTable> diningTables = GetDiningTables().Result.ToList();
        List<Reservation> reservationsByDate = GetReservationsByDateTime(reservationTime, null).Result.ToList();

        foreach (Reservation reservation in reservationsByDate)
        {
            foreach (ReservationDiningTable rdt in reservation.ReservationDiningTables)
            {
                if (rdt.DiningTableId == 0) continue;
                diningTables.RemoveAll(dt => dt.Id == rdt.DiningTableId);
            }
        }

        return (diningTables.Count > 0) ? diningTables.PickRandom() : null;
    }
}