using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3WebAPI.Data {
    public interface IOrderService {
        Task<Order> CreateOrderAsync(OrderModel orderModel);
        Task<IList<Order>> GetOrdersAsync(int index, int id, string status);
        Task<Order> GetOrderAsync(int orderId);
        Task<Order> UpdateOrderAsync(UpdateOrderModel order);
        Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index);
        Task UpdateOrderItemsAsync(Order order);
        
        
    }
}