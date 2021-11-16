using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface IOrderService {
        ShoppingCart ShoppingCart { get; init; }
        Task<Order> CreateOrderAsync(OrderModel order);
        Task<Order> CheckStock(OrderModel order);
    }
}