using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3UI.Data;

namespace SEP3WebAPI.Data {
    public interface ICustomerDAO {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(CustomerModel customer);
        Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
        Task<Item> AddToWishlistAsync(int customerId, int itemId);
        Task RemoveWishlistedItemAsync(int customerId, int itemId);
        Task<Item> AddToShoppingCartAsync(Item item, int customerId);
        Task<IList<Item>> GetShoppingCartAsync(int customerId);
        Task<Item> UpdateShoppingCartAsync(Item item, int itemId, int customerId);
        Task RemoveFromShoppingCartAsync(int itemId, int customerId);
        Task<IList<Notification>> GetNotificationsAsync(int customerId, int index);
    }
}