using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<bool> DeleteOrder(long id);
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> GetOrder(long id);
        Task<bool> UpdateOrder(long id, Order order);
    }
}