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
            return await restService.GetAsync<Order>($"orders/{orderId}");
        }

        public async Task<Order> UpdateOrderAsync(UpdateOrderModel orderModel) {
            return await restService.PutAsync<UpdateOrderModel,Order>(orderModel,$"orders/{orderModel.CustomerId}/{orderModel.OrderId}");
        }

        public async Task<Order> ReturnItemsAsync(ReturnItemsModel model) {
            return await restService.PutAsync<ReturnItemsModel, Order>(model, $"orders/{model.OrderId}");
        }
        
        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            return await restService.GetAsync<IList<Order>>($"orders/{customerId}/order?index={index}");
        }
    }
}