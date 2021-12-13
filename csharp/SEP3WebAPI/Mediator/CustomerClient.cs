using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class CustomerClient : ICustomerClient {

        private IClient client;

        public CustomerClient(IClient client) {
            this.client = client;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            CustomerMessage req = new CustomerMessage() {
                Type = "login",
                Customer = new Customer() {
                    Email = email,
                    Password = password
                }
            };
            return ((CustomerMessage) client.Send(req)).Customer;
        }
        
        public async Task<Customer> GetCustomerAsync(int customerId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomer",
                Customer = new Customer() {
                    Id = customerId
                }
            };
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerMessage req = new CustomerMessage() {
                Type = "register",
                Customer = customer
            };
            return ((CustomerMessage)client.Send(req)).Customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer) {
            CustomerMessage req = null;
            if (customer.Role != null) {
                 req = new CustomerMessage() {
                    Type = "updateRole",
                    Customer = customer
                };
            }
            else {
                 req = new CustomerMessage() {
                    Type = "updateCustomer",
                    Customer = customer
                };
            }
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<IList<Customer>> GetCustomerWithWishlistItemAsync(int itemId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "customerWithWishlistItem",
                ItemId = itemId
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotifications",
                Index = index,
                Customer = new Customer() {
                    Id = customerId
                }
            };
            return ((CustomerMessage) client.Send(req)).Notifications;
        }

        public async Task<IList<Customer>> GetAdminsAsync() {
            CustomerMessage req = new CustomerMessage() {
                Type = "getAdmins",
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<Notification> GetSpecificNotificationAsync(Customer customer, int notificationId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotification",
                Customer = customer,
                Notification = new Notification() {
                    Id = notificationId
                }
            };
            return ((CustomerMessage) client.Send(req)).Notification;
        }

        public async Task SendNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "sendNotification",
                Customer = customer,
                Notification = notification
            };
            client.Send(req);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "updateSeenNotification",
                Customer = customer,
                Notification = notification
            };
            return ((CustomerMessage) client.Send(req)).Notification;
        }

        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomersByIndex",
                Index = index
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }
    }
}