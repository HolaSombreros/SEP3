using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface ICustomerService {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(CustomerModel customer);
        Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer);
        Task<Item> AddToWishlistAsync(int customerId, Item item);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
        Task RemoveWishlistedItem(int customerId, int itemId);
        public Task<Item> AddToShoppingCartAsync(Item item, int customerId);
        public Task<IList<Item>> GetShoppingCartAsync(int customerId);
        public Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId);
        public Task RemoveFromShoppingCartAsync(int itemId, int customerId);
        Task<IList<Notification>> GetNotificationsAsync(int customerId, int index);
        Task<IList<Customer>> GetCustomersByIndexAsync(int index);
        Task<IList<Order>> GetOrdersByCustomer(int customerId, int index);
    }
}