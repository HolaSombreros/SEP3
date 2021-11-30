using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
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

        public async Task<IList<Order>> GetOrdersAsync(int index) {
            IList<Order> orders = await restService.GetAsync<IList<Order>>($"orders?index={index}");
            return orders;
        }
    }
}