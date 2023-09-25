using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.Services
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<bool> DeleteOrder(int id);
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> GetOrder(int id);
        Task<bool> UpdateOrder(int id, Order order);
    }
}