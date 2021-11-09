using System.Threading.Tasks;
using SEP3Library.Model;

namespace SEP3UI.Data {
    public class CustomerService : ICustomerService {
        private readonly IRestService restService;
        
        public CustomerService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await restService.GetAsync<Customer>("customers");
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            throw new System.NotImplementedException();
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            Customer added = await restService.PostAsync<Customer, Customer>(customer, "customers");
            return added;
        }
    }
}