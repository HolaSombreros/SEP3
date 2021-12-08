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
                Service = "order", 
                Type = "purchase", 
                Order = order
            };
            return ((OrderMessage) client.Send(req)).Order;
        }

        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            OrderMessage req = new OrderMessage() {
                Service = "order",
                Type = "getAll",
                Index = index,
                CustomerId = id,
                Status = status
            };
            return ((OrderMessage) client.Send(req)).Orders;
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            OrderMessage req = new OrderMessage() {
                Service = "order",
                Type = "get",
                Order = new Order() {
                    Id = orderId
                }
            };
            return ((OrderMessage) client.Send(req)).Order;
        }
        
        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            OrderMessage req = new OrderMessage() {
                Type = "getAllByCustomer",
                Service = "order",
                CustomerId = customerId,
                Index = index
            };
            return ((OrderMessage) client.Send(req)).Orders;
        }
        
        public async Task<Order> UpdateOrderAsync(Order order) {
            OrderMessage req = new OrderMessage() {
                Type = "update",
                Service = "order",
                Order = order
            };
            return ((OrderMessage) client.Send(req)).Order;
        }
    }
}