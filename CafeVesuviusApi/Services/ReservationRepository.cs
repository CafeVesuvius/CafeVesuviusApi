using AutoMapper;
using CafeVesuviusApi.Context;
using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Services.Utilities;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services;

public class ReservationRepository : IReservationRepository
{
    private readonly CafeVesuviusContext _context;

    public ReservationRepository(CafeVesuviusContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ReservationDTO>> GetReservations()
    {
        List<ReservationDTO> reservationDtos = new List<ReservationDTO>();
        foreach (Reservation reservation in await _context.Reservations.ToListAsync())
        {
            var reservationConfig = new MapperConfiguration(cfg => cfg.CreateMap<Reservation, ReservationDTO>().ForMember(x => x.ReservationDiningTables, opt => opt.Ignore()));
            var reservationMapper = new Mapper(reservationConfig);
            ReservationDTO reservationDto = reservationMapper.Map<ReservationDTO>(reservation);
            
            foreach (ReservationDiningTable rdt in await _context.ReservationDiningTables.Where(rdt => rdt.ReservationID == reservation.Id).ToListAsync())
            {
                DiningTable? diningTable = await _context.DiningTables.Where(item => item.Id == rdt.DiningTableID).SingleOrDefaultAsync();
                if (diningTable == null) continue;

                reservationDto.ReservationDiningTables.Add(diningTable);
            }
            
            reservationDtos.Add(reservationDto);
        }

        return reservationDtos;
    }
    
    public async Task<IEnumerable<Reservation>> GetReservationsByDateTime(DateTime from, DateTime? to)
    {
        List<Reservation>? reservations = await _context.Reservations.Where(reservation =>
            reservation.Time.Date >= from.Date && reservation.Time.Date <= ((to.HasValue) ? to.Value.Date : from.Date)).ToListAsync();
        
        if (reservations.Count > 0)
        {
            foreach (Reservation reservation in reservations)
            {
                reservation.ReservationDiningTables = await _context.ReservationDiningTables.Where(reservationDiningTable => reservationDiningTable.ReservationID == reservation.Id).ToListAsync();
            }
        }
        return reservations;
    }
    
    public async Task<Reservation> GetReservation(int id)
    {
        Reservation? reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return reservation;
        }
        reservation.ReservationDiningTables = await _context.ReservationDiningTables.Where(reservationDiningTable => reservationDiningTable.ReservationID == reservation.Id).ToListAsync();

        return reservation;
    }
    
    public async Task<Reservation> PostReservation(Reservation reservation)
    {
        DiningTable reservationTable = await GetAvailableDiningTable(reservation.Time);

        if (reservationTable.Id != 0)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            ReservationDiningTable reservationDiningTable = new();
            reservationDiningTable.ReservationID = reservation.Id;
            reservationDiningTable.DiningTableID = reservationTable.Id;
            
            reservation.ReservationDiningTables.Add(reservationDiningTable);

            bool status = await PutReservation(reservation.Id, reservation);
            if (!status) return await Task.FromResult<Reservation>(null);
        }
        else return await Task.FromResult<Reservation>(null);
        return reservation;
    }
    
    public async Task<bool> PutReservation(int id, Reservation reservation)
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
    
    public async Task<bool> DeleteReservation(int id)
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
    
    public async Task<bool> PutDiningTable(int id, DiningTable diningTable)
    {
        _context.Entry(diningTable).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(_context.DiningTables?.Any(e => e.Id == id)).GetValueOrDefault())
            {
                return false;
            }
        }
        return true;
    }
    
    public async Task<bool> DeleteDiningTable(int id)
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
    
    public async Task<IEnumerable<DiningTable>> GetAvailableDiningTables(DateTime reservationTime)
    {
        List<DiningTable> diningTables = (List<DiningTable>)await GetDiningTables();
        List<Reservation> reservationsByDate = (List<Reservation>)await GetReservationsByDateTime(reservationTime, null);

        foreach (Reservation reservation in reservationsByDate)
        {
            if (Math.Abs((reservation.Time - reservationTime).TotalHours) > 2) continue;
            foreach (ReservationDiningTable rdt in reservation.ReservationDiningTables)
            {
                if (rdt.DiningTableID == 0) continue;
                diningTables.RemoveAll(dt => dt.Id == rdt.DiningTableID);
            }
        }

        return diningTables;
    }

    public async Task<IEnumerable<DiningTable>> GetAvailableDiningTablesByDate(DateOnly reservationDate)
    {
        DateTime startOfDay = reservationDate.ToDateTime(new TimeOnly(10, 0, 0));
        DateTime endOfDay = reservationDate.ToDateTime(new TimeOnly(22, 0, 0));

        List<DiningTable> availableDiningTables = new List<DiningTable>();
        List<DiningTable> diningTables = (List<DiningTable>)await GetDiningTables();
        List<Reservation> reservationsByDay = (List<Reservation>)await GetReservationsByDateTime(startOfDay, endOfDay);

        foreach (Reservation reservation in reservationsByDay)
        {
            DateTime iDay = startOfDay;
            bool availableFound = false;

            while (iDay != endOfDay)
            {
                if (availableFound) break;
                if (Math.Abs((reservation.Time - iDay).TotalHours) > 2)
                {
                    foreach (ReservationDiningTable rdt in reservation.ReservationDiningTables)
                    {
                        if (rdt.DiningTableID == 0) continue;
                        DiningTable? diningTable = diningTables.Find(dt => dt.Id == rdt.DiningTableID);

                        if (diningTable != null) {
                            availableDiningTables.Add(diningTable);
                        }
                    }
                    availableFound = true;
                };

                iDay = iDay.AddHours(2);
            }
        }

        return availableDiningTables;
    }

    public async Task<DiningTable> GetAvailableDiningTable(DateTime reservationTime)
    {
        List<DiningTable> diningTables = (List<DiningTable>)await GetAvailableDiningTables(reservationTime);
        return (diningTables.Any()) ? diningTables.PickRandom() : await Task.FromResult<DiningTable>(null);
    }

    public async Task<AvailableResponse> IsAvailableByDateTime(DateTime reservationTime)
    {
        List<DiningTable> diningTables = (List<DiningTable>)await GetAvailableDiningTables(reservationTime);

        AvailableResponse availableResponse = new AvailableResponse();
        availableResponse.IsAvailable = diningTables.Any();
        availableResponse.Reason = availableResponse.IsAvailable ? "No reason given." : "Reservation is too busy at the time.";

        return await Task.FromResult<AvailableResponse>(availableResponse);
    }

    public async Task<AvailableResponse> IsAvailableByDateOnly(DateOnly reservationDate)
    {
        List<DiningTable> diningTables = (List<DiningTable>)await GetAvailableDiningTablesByDate(reservationDate);

        AvailableResponse availableResponse = new AvailableResponse();
        availableResponse.IsAvailable = diningTables.Any();
        availableResponse.Reason = availableResponse.IsAvailable ? "No reason given." : "Reservation is too busy the whole day.";

        return await Task.FromResult<AvailableResponse>(availableResponse);
    }
}