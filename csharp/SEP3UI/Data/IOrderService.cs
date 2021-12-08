using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface IOrderService {
        ShoppingCart ShoppingCart { get; init; }
        Task<Order> CreateOrderAsync(OrderModel order);
        Task<IList<Order>> GetOrdersAsync(int index, int id, string status);
        Task<Order> GetOrderAsync(int orderId);
        Task<Order> UpdateOrderAsync(UpdateOrderModel orderModel);
        Task<Order> ReturnItemsAsync(ReturnItemsModel model);
    }
}