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
                Customers = new List<Customer>() {
                    new() {
                        Email = email,
                        Password = password
                    }
                }
            };
            return ((CustomerMessage) client.Send(req)).Customers[0];
        }
        
        public async Task<Customer> GetCustomerAsync(int customerId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomer",
                Customers = new List<Customer>() {
                    new() {
                        Id = customerId
                    }
                }
            };
            return ((CustomerMessage) client.Send(req)).Customers[0];
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerMessage req = new CustomerMessage() {
                Type = "register",
                Customers = new List<Customer>(){customer}
            };
            return ((CustomerMessage)client.Send(req)).Customers[0];
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer) {
            CustomerMessage req = null;
            if (customer.Role != null) {
                 req = new CustomerMessage() {
                    Type = "updateRole",
                    Customers = new List<Customer>(){customer}
                };
            }
            else {
                 req = new CustomerMessage() {
                    Type = "updateCustomer",
                    Customers = new List<Customer>(){customer}
                };
            }
            return ((CustomerMessage) client.Send(req)).Customers[0];
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
                Customers = new List<Customer>() {
                    new() {
                    Id = customerId
                    }
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
                Customers = new List<Customer>(){customer},
                Notifications = new List<Notification>() {
                    new () {
                        Id = notificationId
                    }
                }
            };
            return ((CustomerMessage) client.Send(req)).Notifications[0];
        }

        public async Task SendNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "sendNotification",
                Customers = new List<Customer>(){customer},
                Notifications = new List<Notification>() {notification}
            };
            client.Send(req);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "updateSeenNotification",
                Customers = new List<Customer>(){customer},
                Notifications = new List<Notification>() {notification}
            };
            return ((CustomerMessage) client.Send(req)).Notifications[0];
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