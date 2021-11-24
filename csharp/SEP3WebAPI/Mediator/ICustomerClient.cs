using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface ICustomerClient { 
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task<IList<Item>> GetCustomerWishlistAsync(Customer customer);
        Task<Item> AddToWishlist(int customerId, int itemId);
        Task RemoveWishlistedItemAsync(Customer customer, Item item);
        Task<Item> AddToShoppingCartAsync(Item item, Customer customer);
        Task<IList<Item>> GetShoppingCartAsync(Customer customer);
        Task<Item> UpdateShoppingCartAsync(Item item, Customer customer);
        Task RemoveFromShoppingCartAsync(Item item, Customer customer);
    }
}