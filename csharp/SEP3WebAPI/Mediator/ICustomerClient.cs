using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface ICustomerClient { 
        Task<Customer> GetCustomerAsync(string email, string password);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<IList<Item>> GetCustomerWishlistAsync(int customerId);
    }
}