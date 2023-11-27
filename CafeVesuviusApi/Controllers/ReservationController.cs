using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Services;
using Microsoft.AspNetCore.Authorization;
using CafeVesuviusApi.DTOs;

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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();
            Reservation reservation = await _reservationRepository.GetReservation(id);

            if (reservation == null) return NotFound();

            return Ok(reservation);
        }

        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetReservations()
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();
            return Ok(await _reservationRepository.GetReservations());
        }

        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] ReservationRequest reservationRequest)
        {
            if (await _reservationRepository.GetReservations() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _reservationRepository.PostReservation(reservationRequest));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }
            bool success = await _reservationRepository.PutReservation(id, reservation);

            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (await _reservationRepository.GetReservations() == null) return NotFound();

            bool success = await _reservationRepository.DeleteReservation(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpGet("DiningTable/All")]
        public async Task<IActionResult> GetDiningTables()
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();
            return Ok(await _reservationRepository.GetDiningTables());
        }

        [Authorize]
        [HttpGet("DiningTable/Available/{reservationTime}")]
        public async Task<IActionResult> GetAvailableDiningTables(DateTime reservationTime)
        {
            if (await _reservationRepository.GetAvailableDiningTables(reservationTime) == null) return NotFound();
            return Ok(await _reservationRepository.GetAvailableDiningTables(reservationTime));
        }

        [Authorize]
        [HttpPost("DiningTable")]
        public async Task<IActionResult> PostDiningTable(DiningTable diningTable)
        {
            if (await _reservationRepository.GetDiningTables() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _reservationRepository.PostDiningTable(diningTable));
        }

        [Authorize]
        [HttpPut("DiningTable/{id}")]
        public async Task<IActionResult> PutDiningTable(int id, DiningTable diningTable)
        {
            if (id != diningTable.Id)
            {
                return BadRequest();
            }
            bool success = await _reservationRepository.PutDiningTable(id, diningTable);

            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("DiningTable/{id}")]
        public async Task<IActionResult> DeleteDiningTable(int id)
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();

            bool success = await _reservationRepository.DeleteDiningTable(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("IsAvailableToday/{reservationTime}")]
        public async Task<IActionResult> IsAvailableToday(DateTime reservationTime)
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();
            return Ok(await _reservationRepository.IsAvailableByDateOnly(DateOnly.FromDateTime(reservationTime)));
        }

        [HttpGet("IsAvailableAtTime/{reservationTime}")]
        public async Task<IActionResult> IsAvailableAtTime(DateTime reservationTime)
        {
            if (await _reservationRepository.GetDiningTables() == null) return NotFound();
            return Ok(await _reservationRepository.IsAvailableByDateTime(reservationTime));
        }
    }
}
