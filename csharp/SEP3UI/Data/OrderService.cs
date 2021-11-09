using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public class OrderService : IOrderService {
        public ShoppingCart ShoppingCart { get; init; }

        public OrderService() {
            ShoppingCart = new ShoppingCart();
        }

        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            Order newOrder = await RestService.PostAsync<OrderModel, Order>(orderModel, "orders");
            return newOrder;
        }
    }
}