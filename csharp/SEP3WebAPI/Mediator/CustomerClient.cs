using System.Collections.Generic;
using System.Threading.Tasks;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class CustomerClient : ICustomerClient {

        private IClient client;
        private const string service = "customer";

        public CustomerClient(IClient client) {
            this.client = client;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            CustomerMessage req = new CustomerMessage() {
                Type = "login",
                Service = service,
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
                Service = service,
                Customer = new Customer() {
                    Id = customerId
                }
            };
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerMessage req = new CustomerMessage() {
                Type = "register",
                Service = service,
                Customer = customer
            };
            return ((CustomerMessage)client.Send(req)).Customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer) {
            CustomerMessage req = null;
            if (customer.Role != null) {
                 req = new CustomerMessage() {
                    Type = "updateRole",
                    Service = service,
                    Customer = customer
                };
            }
            else {
                 req = new CustomerMessage() {
                    Type = "updateCustomer",
                    Service = service,
                    Customer = customer
                };
            }
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<IList<Customer>> GetCustomerWithWishlistItemAsync(int itemId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "customerWithWishlistItem",
                Service = service,
                ItemId = itemId
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotifications",
                Service = service,
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
                Service = service
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<Notification> GetSpecificNotificationAsync(Customer customer, int notificationId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotification",
                Service = service,
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
                Service = service,
                Customer = customer,
                Notification = notification
            };
            client.Send(req);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "updateSeenNotification",
                Service = service,
                Customer = customer,
                Notification = notification
            };
            return ((CustomerMessage) client.Send(req)).Notification;
        }

        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomersByIndex",
                Service = service,
                Index = index
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }
    }
}