using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3WebAPI.Mediator {
    public interface ICustomerClient {
        public Task<Customer> GetCustomerAsync(string email, string password);
        public Task<Customer> AddCustomerAsync(Customer customer);
    }
}