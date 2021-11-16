using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private IClient client;
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            return await client.GetItemsAsync(index);
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemsId) {
            return await client.GetItemsByIdAsync(itemsId);
        }

        public async Task<Item> GetItemAsync(int id) {
            return await client.GetItemAsync(id);
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            return await client.GetCustomerAsync(email, password);
        }

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            if (customer == null) throw new InvalidDataException("Please provide a customer of the proper format");
            if (!new EmailAddressAttribute().IsValid(customer.Email)) throw new InvalidDataException("Please enter a valid email address");
            Customer c = new Customer() {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = new Address() {
                    Street = customer.Street,
                    Number = customer.Number,
                    City = customer.City,
                    ZipCode = customer.ZipCode
                },
                PhoneNumber = customer.PhoneNumber,
                Password = customer.Password,
                Role = customer.Role 
            };
            return await client.AddCustomerAsync(c);
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            return await client.GetCustomerWishlistAsync(customer);
        }

        public async Task RemoveWishlistedItem(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await client.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            await client.RemoveWishlistedItem(customer, item);
        }

        public async Task<Book> GetBookAsync(int id) {
            return await client.GetBookAsync(id);
        }

        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            if (!new EmailAddressAttribute().IsValid(orderModel.Email)) throw new InvalidDataException("Please enter a valid email address");
            int[] ids = new int[orderModel.Items.Count];
            int i = 0;
            foreach (var item in orderModel.Items) {
                ids[i] = item.Id;
                i++;
            }
            IList<Item> items = (await client.GetItemsByIdAsync(ids)).OrderBy(i => i.Id).ToList();
            orderModel.Items = orderModel.Items.OrderBy(o => o.Id).ToList();
            for(int j=0; j< orderModel.Items.Count; j++) {
                if (orderModel.Items[j].Quantity > items[j].Quantity)
                    throw new InvalidDataException("Item out of stock" + orderModel.Items[j].Name +
                                                   " only this amount is available " + items[j].Quantity);

            }
            
            Order order = new Order() {
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                Email = orderModel.Email,
                Address = new Address() {
                    Street = orderModel.Street,
                    Number = orderModel.Number,
                    City = orderModel.City,
                    ZipCode = orderModel.ZipCode
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
                OrderStatus = OrderStatus.Pending
            };

            return await client.CreateOrderAsync(order);
        }
    }
}