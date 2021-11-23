using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3UI.Authentication;

namespace SEP3UI.Data {
    public class CustomerService : ICustomerService {
        private readonly IRestService restService;
        
        public CustomerService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Customer customer = await restService.GetAsync<Customer>($"customers?email={email}&password={password}");
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            Customer customer = await restService.GetAsync<Customer>($"customers/{customerId}");
            return customer;
        }

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            Customer added = await restService.PostAsync<CustomerModel, Customer>(customer, "customers");
            return added;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(int customerId) {
            IList<Item> wishlist = await restService.GetAsync<List<Item>>($"customers/{customerId}/wishlist");
            return wishlist;
        }

        public async Task RemoveWishlistedItem(int customerId, int itemId) {
            await restService.DeleteAsync($"customers/{customerId}/wishlist/{itemId}");
        }
    }
}