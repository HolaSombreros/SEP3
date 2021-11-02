using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private Client client;
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync() {
            return await client.GetItemsAsync();
        }

        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            
            Order order = new Order() {
                User = new Customer() {
                    FirstName = orderModel.FirstName,
                    LastName = orderModel.LastName,
                    Email = orderModel.Email,
                    Address = new Address() {
                        Street = orderModel.Street,
                        Number = orderModel.Number,
                        City = orderModel.City,
                        ZipCode = orderModel.ZipCode
                    }
                },
                DateTime = new MyDateTime() {
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Day = DateTime.Now.Day,
                    Hour = DateTime.Now.Hour,
                    Minute = DateTime.Now.Minute,
                    Second = DateTime.Now.Second
                },
                Items = orderModel.Items,
                OrderStatus = OrderStatus.PENDING
            };

            return await client.CreateOrderAsync(order);
        }
    }
}