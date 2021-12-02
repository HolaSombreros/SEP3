using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;

namespace SEP3WebAPI.Mediator {
    public interface ICustomerClient { 
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<IList<Item>> GetCustomerWishlistAsync(Customer customer);
        Task<Item> AddToWishlist(int customerId, int itemId);
        Task RemoveWishlistedItemAsync(Customer customer, Item item);
        Task<Item> AddToShoppingCartAsync(Item item, Customer customer);
        Task<IList<Item>> GetShoppingCartAsync(Customer customer);
        Task<Item> UpdateShoppingCartAsync(Item item, Customer customer);
        Task RemoveFromShoppingCartAsync(Item item, Customer customer);
        Task<IList<Customer>> GetCustomersByIndexAsync(int index);
        Task<IList<Notification>> GetNotificationsAsync(int customerId, int index);
        Task<IList<Customer>> GetAdminsAsync();
        Task<Notification> GetSpecificNotificationAsync(Customer customer, int notificationId);
        Task SendNotificationAsync(Customer customer, Notification notification);
        Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification);
        Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index);
    }
}