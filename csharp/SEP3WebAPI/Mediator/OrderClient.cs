using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class OrderClient : IOrderClient {

        private IClient client;

        public OrderClient(IClient client) {
            this.client = client;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            OrderMessage req = new OrderMessage() {
                Type = "purchase", 
                Orders = new List<Order>() {order}
            };
            return ((OrderMessage) client.Send(req)).Orders[0];
        }

        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            OrderMessage req = new OrderMessage() {
                Type = "getAll",
                Index = index,
                CustomerId = id,
                Status = status
            };
            return ((OrderMessage) client.Send(req)).Orders;
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            OrderMessage req = new OrderMessage() {
                Type = "get",
                Orders = new List<Order>() {
                    new() {
                        Id = orderId
                    }
                }
            };
            Order message = ((OrderMessage) client.Send(req)).Orders[0];
            return message;
        }
        
        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            OrderMessage req = new OrderMessage() {
                Type = "getAllByCustomer",
                CustomerId = customerId,
                Index = index
            };
            return ((OrderMessage) client.Send(req)).Orders;
        }

        public async Task UpdateOrderItemsAsync(Order order) {
            OrderMessage req = new OrderMessage() {
                Type = "returnItems",
                Orders = new List<Order>() {order}
            };
            client.Send(req);
        }

        public async Task<Order> UpdateOrderAsync(Order order) {
            OrderMessage req = new OrderMessage() {
                Type = "update",
                Orders = new List<Order>() {order}
            };
            return ((OrderMessage) client.Send(req)).Orders[0];
        }
    }
}