using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Services;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            if (await _orderRepository.GetOrders() == null) return NotFound();
            return Ok(await _orderRepository.GetOrders());
        }


        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            if (await _orderRepository.GetOrders() == null) return NotFound();
            var order = await _orderRepository.GetOrder(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id) return BadRequest();
            if(!(await _orderRepository.UpdateOrder(id, order))) return BadRequest();
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(Order order)
        {
            if (await _orderRepository.GetOrders() == null) return NotFound();
            await _orderRepository.AddOrder(order);
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (await _orderRepository.GetOrders() == null) return NotFound();
            if(!(await _orderRepository.DeleteOrder(id))) return BadRequest();
            return NoContent();
        }
    }
}
