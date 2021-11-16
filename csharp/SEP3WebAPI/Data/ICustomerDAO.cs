using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;
using SEP3UI.Data;

namespace SEP3WebAPI.Data {
    public interface ICustomerDAO {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> AddCustomerAsync(CustomerModel customer);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
        Task RemoveWishlistedItem(int customerId, int itemId);
    }
}