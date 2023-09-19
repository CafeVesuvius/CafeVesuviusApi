using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<bool> DeleteOrder(long id);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order?> GetOrderById(long id);
        Task<bool> UpdateOrder(long id, Order order);
    }
}