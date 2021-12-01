using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public class CustomerService : ICustomerService {
        private readonly IRestService restService;
        
        public CustomerService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await restService.GetAsync<Customer>($"customers?email={email}&password={password}");
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            Customer customer = await restService.GetAsync<Customer>($"customers/{customerId}");
            return customer;
        }

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            Customer added = await restService.PostAsync<CustomerModel, Customer>(customer, "customers");
            return added;
        }

        public async Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer) {
            Customer updated = await restService.PutAsync<UpdateCustomerModel, Customer>(customer, $"customers/{customerId}");
            return updated;
        }

        public async Task<Item> AddToWishlistAsync(int customerId, Item item) {
            Item item1 = await restService.PutAsync<Item, Item>(item,$"customers/{customerId}/wishlist/{item.Id}");
            return item;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            IList<Item> wishlist = await restService.GetAsync<List<Item>>($"customers/{customerId}/wishlist");
            return wishlist;
        }

        public async Task RemoveWishlistedItem(int customerId, int itemId) {
            await restService.DeleteAsync($"customers/{customerId}/wishlist/{itemId}");
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, int customerId) {
            Console.WriteLine("customerservice");
            Item added = await restService.PutAsync<Item, Item>(item, $"customers/{customerId}/shoppingbasket");
            return added;
        }

        public async Task<IList<Item>> GetShoppingCartAsync(int customerId) {
            return await restService.GetAsync<List<Item>>($"customers/{customerId}/shoppingbasket");
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId) {
            Item updated = await restService.PutAsync<Item, Item>(item, $"customers/{customerId}/shoppingbasket/{itemId}");
            return updated;
        }

        public async Task RemoveFromShoppingCartAsync(int itemId, int customerId) {
            await restService.DeleteAsync($"customers/{customerId}/shoppingbasket/{itemId}");
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            return await restService.GetAsync<IList<Notification>>($"customers/{customerId}/notifications?index={index}");
        }
    }
}