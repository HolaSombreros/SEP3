using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEP3Library.Model;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private Client client;
        // TODO - Should we implement a queuing system either here or in the "Client" class to keep track of the order of requests?
        // To ensure each response reaches the correct client
        
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            return await client.GetItemsAsync();
        }
        
        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new ArgumentNullException("Please specify an order of the proper format");
            // if (orderModel.Items.Count < 1) throw new ArgumentNullException("Your order must contain at least 1 item");

            Order order = new Order() {
                DateTime = DateTime.Now,
                User = orderModel.Customer,
                Items = orderModel.Items,
                OrderStatus = OrderStatus.PENDING
            };

            return await client.CreateOrderAsync(order);
        }
    }
}