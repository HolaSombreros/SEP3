using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public class OrderService : IOrderService {
        private readonly IRestService restService;
        public ShoppingCart ShoppingCart { get; init; }

        public OrderService(IRestService restService) {
            this.restService = restService;
            ShoppingCart = new ShoppingCart();
        }
        
        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            Order newOrder = await restService.PostAsync<OrderModel, Order>(orderModel, "orders");
            return newOrder;
        }

        public async Task<Order> CheckStock(OrderModel order) {
            string endpoint = "items?";
            for(int i = 0; i < order.Items.Count; i++) {
                if (i == order.Items.Count) 
                    endpoint += "itemIds=" + order.Items[i];
                else
                    endpoint += "itemIds=" + order.Items[i] + "&";
            }
            Order checkOrder = await restService.GetAsync<Order>(endpoint);
            return checkOrder;
        }
    }
}