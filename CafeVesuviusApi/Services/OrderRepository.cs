using AutoMapper;
using CafeVesuviusApi.Context;
using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            List<OrderDTO> orderDtos = new List<OrderDTO>();
            
            foreach (Order order in orders)
            {
                var orderConfig = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>().ForMember(x => x.OrderLines, opt => opt.Ignore()));
                var orderMapper = new Mapper(orderConfig);
                OrderDTO orderDto = orderMapper.Map<OrderDTO>(order);
                
                foreach (OrderLine line in await _context.OrderLines.Where(line => line.OrderID == order.Id).ToListAsync())
                {
                    var orderLineConfig = new MapperConfiguration(cfg => cfg.CreateMap<OrderLine, OrderLineDTO>());
                    var orderLineMapper = new Mapper(orderLineConfig);
                    OrderLineDTO orderLineDto = orderLineMapper.Map<OrderLineDTO>(line);
                    
                    MenuItem? menuItem = await _context.MenuItems.Where(item => item.Id == line.MenuItemID).SingleOrDefaultAsync();
                    if (menuItem == null) continue;
                    
                    orderLineDto.MenuItem = menuItem;
                    orderDto.OrderLines.Add(orderLineDto);
                }
                
                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }

        //Get order by id
        public async Task<OrderDTO> GetOrder(int id)
        {
            Order? order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
            if (order == null) return await Task.FromResult<OrderDTO>(null); // Order wasn't found
            
            var orderConfig = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>().ForMember(x => x.OrderLines, opt => opt.Ignore()));
            var orderMapper = new Mapper(orderConfig);
            OrderDTO orderDto = orderMapper.Map<OrderDTO>(order);
            
            foreach (OrderLine line in await _context.OrderLines.Where(line => line.OrderID == order.Id).ToListAsync())
            {
                var orderLineConfig = new MapperConfiguration(cfg => cfg.CreateMap<OrderLine, OrderLineDTO>());
                var orderLineMapper = new Mapper(orderLineConfig);
                OrderLineDTO orderLineDto = orderLineMapper.Map<OrderLineDTO>(line);
                
                MenuItem? menuItem = await _context.MenuItems.Where(item => item.Id == line.MenuItemID).SingleOrDefaultAsync();
                if (menuItem == null) continue;
                
                orderLineDto.MenuItem = menuItem;
                orderDto.OrderLines.Add(orderLineDto);
            }
            return orderDto;
        }

        //Update order
        public async Task<bool> UpdateOrder(int id, Order order)
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
        
        public async Task<OrderLine> AddOrderLine(OrderLine orderLine)
        {
            if (!(_context.Orders?.Any(e => e.Id == orderLine.OrderID)).GetValueOrDefault())
                return await Task.FromResult<OrderLine>(null);
            
            _context.OrderLines.Add(orderLine);
            await _context.SaveChangesAsync();
            return orderLine;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
