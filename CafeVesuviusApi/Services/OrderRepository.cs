using CafeVesuviusApi.Models;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace CafeVesuviusApi.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CafeVesuviusContext _context;

        public OrderRepository(CafeVesuviusContext context)
        {
            _context = context;
        }

        //Gets All orders from database
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();

            if (orders.Any())
            {
                foreach (Order order in orders)
                {
                    order.OrderLines = await _context.OrderLines.Where(line => line.OrderId == order.Id).ToListAsync();
                    foreach (OrderLine line in order.OrderLines)
                    {
                        line.MenuItem = await _context.MenuItems.Where(item => item.Id == line.MenuItemId).SingleOrDefaultAsync();
                    }
                }
            }

            return orders;
        }

        //Get order by id
        public async Task<Order?> GetOrderById(long id)
        {
            Order? order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                order.OrderLines = await _context.OrderLines.Where(line => line.OrderId == order.Id).ToListAsync();
                foreach (OrderLine line in order.OrderLines)
                {
                    line.MenuItem = await _context.MenuItems.Where(item => item.Id == line.MenuItemId).SingleOrDefaultAsync();
                }
            }
            return order;
        }

        //Update order
        public async Task<bool> UpdateOrder(long id, Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault()) return false;
            }
            return true;
        }

        public async Task<Order> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
