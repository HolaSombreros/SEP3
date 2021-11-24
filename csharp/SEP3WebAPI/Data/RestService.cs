using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;
using SEP3WebAPI.Mediator.Requests;

namespace SEP3WebAPI.Data {
    public class RestService : IRestService {
        private IClient client;
        
        public RestService() {
            client = new Client();
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            return await client.GetItemsAsync(index);
        }

        public async Task<Item> GetItemAsync(int id) {
            return await client.GetItemAsync(id);
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await client.GetCustomerAsync(email, password);
            if (customer == null)
                throw new Exception("Email not registered");
            if (customer.Password.Equals(password))
                return customer;
            throw new Exception("Wrong password");
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            return customer;
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

        public async Task<Customer> UpdateCustomerAsync(int customerId, CustomerModel customer) {
            if (customer == null) throw new InvalidDataException("Please provide a customer of the proper format");
            if (!new EmailAddressAttribute().IsValid(customer.Email)) throw new InvalidDataException("Please enter a valid email address");
            
            Customer updated = await client.GetCustomerAsync(customerId);
            if (updated == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            updated.Id = customerId;
            updated.FirstName = customer.FirstName;
            updated.LastName = customer.LastName;
            updated.Email = customer.Email;
            updated.Address.Street = customer.Street;
            updated.Address.Number = customer.Number;
            updated.Address.ZipCode = customer.ZipCode;
            updated.Address.City = customer.City;
            updated.PhoneNumber = customer.PhoneNumber;
            updated.Password = customer.Password;
            updated.Role = customer.Role;

            await client.UpdateCustomerAsync(updated);
            return updated;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            return await client.GetCustomerWishlistAsync(customer);
        }

        public async Task RemoveWishlistedItemAsync(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await client.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            await client.RemoveWishlistedItemAsync(customer, item);
        }

        public async Task<Book> GetBookAsync(int id) {
            return await client.GetBookAsync(id);
        }

        public async Task<Order> CreateOrderAsync(OrderModel orderModel) {
            if (orderModel == null) throw new InvalidDataException("Please specify an order of the proper format");
            if (orderModel.Items == null || orderModel.Items.Count < 1) throw new InvalidDataException("Your order must contain at least 1 item");
            if (!new EmailAddressAttribute().IsValid(orderModel.Email)) throw new InvalidDataException("Please enter a valid email address");
            
            int[] itemIds = new int[orderModel.Items.Count];
            for (int i = 0; i < itemIds.Length; i++) {
                itemIds[i] = orderModel.Items[i].Id;
            }
            IList<Item> items = await client.GetItemsByIdAsync(itemIds);
            orderModel.Items = orderModel.Items.OrderBy(o => o.Id).ToList();
            for (int i = 0; i < orderModel.Items.Count; i++) {
                if (orderModel.Items[i].Quantity > items[i].Quantity) {
                    if (items[i].Quantity == 0)
                        throw new InvalidDataException("Item " + orderModel.Items[i].Name +
                                                       " is out of stock. The stock will be updated later");
                    
                    throw new InvalidDataException("Item " + orderModel.Items[i].Name +
                                                   " amount exceeds the amount available. Only this amount is available " +
                                                   items[i].Quantity);
                }
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