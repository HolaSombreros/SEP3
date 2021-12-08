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
                Service = "customer",
                Customer = new Customer() {
                    Email = email,
                    Password = password
                }
            };
            return ((CustomerMessage) client.Send(req)).Customer;
        }
        
        public async Task<Customer> GetCustomerAsync(int customerId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "get",
                Service = "customer",
                Customer = new Customer() {
                    Id = customerId
                }
            };
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerMessage req = new CustomerMessage() {
                Type = "register",
                Service = "customer",
                Customer = customer
            };
            return ((CustomerMessage)client.Send(req)).Customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer) {
            CustomerMessage req = null;
            if (customer.Role != null) {
                 req = new CustomerMessage() {
                    Type = "updateRole",
                    Service = "customer",
                    Customer = customer
                };
            }
            else {
                 req = new CustomerMessage() {
                    Type = "update",
                    Service = "customer",
                    Customer = customer
                };
            }
            return ((CustomerMessage) client.Send(req)).Customer;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getWishlist",
                Service = "item",
                Customer = customer
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> AddToWishlist(int customerId, int itemId) {
            ItemMessage req = new ItemMessage() {
                Type = "addWishlist",
                Service = "item",
                Customer = new Customer() {Id = customerId},
                Item = new Item() {Id = itemId}
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task RemoveWishlistedItemAsync(Customer customer, Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "removeWishlist",
                Service = "item",
                Customer = customer,
                Item = item
            };
            client.Send(req);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "addShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task<IList<Item>> GetShoppingCartAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getShoppingCart",
                Service = "item",
                Customer = customer
            };
            return ((ItemMessage) client.Send(req)).Items;
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "editShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            return ((ItemMessage) client.Send(req)).Item;
        }

        public async Task RemoveFromShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "removeShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            client.Send(req);
        }

        public async Task<IList<Notification>> GetNotificationsAsync(int customerId, int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotifications",
                Service = "customer",
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
                Service = "customer"
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<Notification> GetSpecificNotificationAsync(Customer customer, int notificationId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getNotification",
                Service = "customer",
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
                Service = "customer",
                Customer = customer,
                Notification = notification
            };
            client.Send(req);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "updateSeenNotification",
                Service = "customer",
                Customer = customer,
                Notification = notification
            };
            return ((CustomerMessage) client.Send(req)).Notification;
        }

        public async Task<IList<Customer>> GetCustomerWithWishlistItemAsync(int itemId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "customerWithWishlistItem",
                Service = "customer",
                ItemId = itemId
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }

        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomersByIndex",
                Service = "customer",
                Index = index
            };
            return ((CustomerMessage) client.Send(req)).Customers;
        }
    }
}