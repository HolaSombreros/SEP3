using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;

namespace SEP3UI.Data {
    public class CustomerService : ICustomerService {
        private readonly IRestService restService;
        
        public CustomerService(IRestService restService) {
            this.restService = restService;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            return await restService.GetAsync<Customer>($"customers?email={email}&password={password}");
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            return await restService.GetAsync<Customer>($"customers/{customerId}");
        }

        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            return await restService.PostAsync<CustomerModel, Customer>(customer, "customers");
        }

        public async Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer) {
            return await restService.PutAsync<UpdateCustomerModel, Customer>(customer, $"customers/{customerId}");
        }

        public async Task<Notification> UpdateSeenNotificationAsync(int customerId, Notification notification) {
            return await restService.PutAsync<Notification, Notification>(notification, $"customers/{customerId}/notifications/{notification.Id}");
        }
        
        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            return await restService.GetAsync<List<Customer>>($"customers/all?index={index}");
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            return await restService.GetAsync<IList<Notification>>($"customers/{customerId}/notifications?index={index}");
        }

       
    }
}