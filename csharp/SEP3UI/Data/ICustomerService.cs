using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public interface ICustomerService {
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<Customer> AddCustomerAsync(CustomerModel customer);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
    }
}