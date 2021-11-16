using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Model;
using SEP3Library.UIModels;

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

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            Customer added = await restService.PostAsync<CustomerModel, Customer>(customer, "customers");
            return added;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            IList<Item> wishlist = await restService.GetAsync<List<Item>>($"customers/{customerId}/wishlist");
            return wishlist;
        }
    }
}