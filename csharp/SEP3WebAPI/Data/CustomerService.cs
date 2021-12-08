using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class CustomerService : ICustomerService {
        private ICustomerClient client;
        private IItemClient itemClient;
        
        public CustomerService(IItemClient itemClient) {
            this.itemClient = itemClient;
            client = new Client();
        }
        
         public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            return await client.GetCustomerWishlistAsync(customer);
        }

        public async Task<Item> AddToWishlistAsync(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await itemClient.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            return await client.AddToWishlist(customerId, itemId);
        }

        public async Task RemoveWishlistedItemAsync(int customerId, int itemId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await itemClient.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");
            
            await client.RemoveWishlistedItemAsync(customer, item);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item1 = await itemClient.GetItemAsync(item.Id);
            if (item1 == null) throw new NullReferenceException($"No such item found with id: {item.Id}");

            return await client.AddToShoppingCartAsync(item, customer);
        }

        public async Task<IList<Item>> GetShoppingCartAsync(int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            return await client.GetShoppingCartAsync(customer);
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item1 = await itemClient.GetItemAsync(itemId);
            if (item1 == null) throw new NullReferenceException($"No such item found with id: {itemId}");

            return await client.UpdateShoppingCartAsync(item, customer);
        }

        public async Task RemoveFromShoppingCartAsync(int itemId, int customerId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
            
            Item item = await itemClient.GetItemAsync(itemId);
            if (item == null) throw new NullReferenceException($"No such item found with id: {itemId}");

            await client.RemoveFromShoppingCartAsync(item, customer);
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            return await client.GetNotificationsAsync(customerId, index);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(int customerId, int notificationId) {
            Customer customer = await client.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            Notification notification = await client.GetSpecificNotificationAsync(customer, notificationId);
            if (notification == null) throw new NullReferenceException($"No such notification found with id: {notificationId} for the customer: {customerId}");

            notification.Status = "Read";
            return await client.UpdateSeenNotificationAsync(customer, notification);
        }

         public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await client.GetCustomerAsync(email, password);
            return customer;
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

        public async Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer) {
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
        
        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            return await client.GetCustomersByIndexAsync(index);
        }
        
    }
}