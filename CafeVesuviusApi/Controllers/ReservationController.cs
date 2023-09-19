using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Services;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(long id)
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();
            Reservation reservation = await _reservationRepository.GetReservation(id);
            
            if(reservation == null) return NotFound();

            return Ok(reservation);
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetReservations()
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();
            return Ok(await _reservationRepository.GetReservations());
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(long id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }
            bool success = await _reservationRepository.PutReservation(id, reservation);

            if (!success) return NotFound();
            return NoContent();
        }
        
        [HttpPost]
        public async Task<IActionResult> PostReservation(Reservation reservation)
        {
            if (await _reservationRepository.GetReservations() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _reservationRepository.PostReservation(reservation));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();

            bool success = await _reservationRepository.DeleteReservation(id);
            if (!success) return NotFound();
            return NoContent();
        }
        
        [HttpGet("DiningTable/All")]
        public async Task<IActionResult> GetDiningTables()
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();
            return Ok(await _reservationRepository.GetDiningTables());
        }
        
        [HttpPost("DiningTable")]
        public async Task<IActionResult> PostDiningTable(DiningTable diningTable)
        {
            if (await _reservationRepository.GetDiningTables() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _reservationRepository.PostDiningTable(diningTable));
        }
        
        [HttpDelete("DiningTable/{id}")]
        public async Task<IActionResult> DeleteDiningTable(long id)
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();

            bool success = await _reservationRepository.DeleteDiningTable(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
