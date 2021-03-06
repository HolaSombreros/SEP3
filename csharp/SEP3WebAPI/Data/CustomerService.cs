using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3Library.UIModels;
using SEP3WebAPI.Mediator;

namespace SEP3WebAPI.Data {
    public class CustomerService : ICustomerService {
        private ICustomerClient customerClient;
        
        public CustomerService(ICustomerClient customerClient) {
            this.customerClient = customerClient;
        }
        
        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            Customer customer = await customerClient.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            return await customerClient.GetNotificationsAsync(customerId, index);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(int customerId, int notificationId) {
            Customer customer = await customerClient.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            Notification notification = await customerClient.GetSpecificNotificationAsync(customer, notificationId);
            if (notification == null) throw new NullReferenceException($"No such notification found with id: {notificationId} for the customer: {customerId}");

            notification.Status = "Read";
            return await customerClient.UpdateSeenNotificationAsync(customer, notification);
        }

         public async Task<Customer> GetCustomerAsync(string email, string password) {
             Customer customer = await customerClient.GetCustomerAsync(email, password);
            
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(int customerId) {
            Customer customer = await customerClient.GetCustomerAsync(customerId);
            if (customer == null) throw new NullReferenceException($"No such customer found with id: {customerId}");
           
            return customer;
        }

        /**
         * The method creates a new customer based on customer model 
         */
        public async Task<Customer> AddCustomerAsync(CustomerModel customer) {
            if (customer == null) 
                throw new InvalidDataException("Please provide a customer of the proper format");
            
            Customer c = new Customer() {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = new Address() {
                    Street = customer.Street,
                    Number = customer.Number,
                    City = customer.City,
                    ZipCode = customer.ZipCode
                },
                PhoneNumber = customer.PhoneNumber,
                Password = customer.Password,
                Role = customer.Role 
            };

            if (!(await customerClient.GetAdminsAsync()).Any())
                c.Role = "Administrator";
            
            return await customerClient.AddCustomerAsync(c);
        }

        /**
         * The method creates a new customer based on customer model 
         */
        public async Task<Customer> UpdateCustomerAsync(int customerId, UpdateCustomerModel customer) {
            Customer updated = await customerClient.GetCustomerAsync(customerId);
            if (updated == null) throw new NullReferenceException($"No such customer found with id: {customerId}");

            updated.Id = customerId;
            updated.FirstName = customer.FirstName;
            updated.LastName = customer.LastName;
            updated.Email = customer.Email;
            updated.Address.Street = customer.Street;
            updated.Address.Number = customer.Number;
            updated.Address.ZipCode = customer.ZipCode;
            updated.Address.City = customer.City;
            updated.PhoneNumber = customer.PhoneNumber;
            updated.Password = customer.Password;
            updated.Role = customer.Role;

            await customerClient.UpdateCustomerAsync(updated);
            return updated;
        }
        
        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            return await customerClient.GetCustomersByIndexAsync(index);
        }
    }
}