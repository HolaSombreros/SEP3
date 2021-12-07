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
            return await restService.PostAsync<OrderModel, Order>(orderModel, "orders");
        }

        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            return await restService.GetAsync<IList<Order>>($"orders?index={index}&id={id}&status={status}");
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            Order order = await restService.GetAsync<Order>($"orders/{orderId}");
            return order;
        }

        public async Task<Order> UpdateOrderAsync(UpdateOrderModel orderModel) {
            Order order = await restService.PutAsync<UpdateOrderModel,Order>(orderModel,$"orders/{orderModel.CustomerId}/{orderModel.OrderId}");
            return order;
        }

        public async Task<Order> ReturnItemsAsync(ReturnItemsModel model) {
            Order order = await restService.PutAsync<ReturnItemsModel, Order>(model, $"orders/{model.OrderId}");
            return order;
        }
    }
}