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
        Task<Customer> UpdateCustomerAsync(int customerId, CustomerModel customer);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
        Task RemoveWishlistedItemAsync(int customerId, int itemId);
    }
}