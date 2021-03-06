using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface IOrderClient {
        Task<Order> CreateOrderAsync(Order order);
        Task<IList<Order>> GetOrdersAsync(int index, int id, string status);
        Task<Order> GetOrderAsync(int orderId);
        Task<Order> UpdateOrderAsync(Order order);
        Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index);
        Task UpdateOrderItemsAsync(Order order);
    }
}