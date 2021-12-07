using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class Client : IClient {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private object lock1;
        private Message reply;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new object();
        }

        public void Receive(string result) {
            lock (lock1) {
                reply = JsonSerializer.Deserialize<Message>(result,
                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if (reply != null) {
                    switch (reply.Service) {
                        case "item":
                            reply = JsonSerializer.Deserialize<ItemMessage>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "order":
                            reply = JsonSerializer.Deserialize<OrderMessage>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "customer":
                            reply = JsonSerializer.Deserialize<CustomerMessage>(result,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            break;
                        case "faq":
                            reply = JsonSerializer.Deserialize<FAQMessage>(result,
                                new JsonSerializerOptions() {
                                    PropertyNameCaseInsensitive = true
                                });
                            break;
                        case "error":
                            reply = JsonSerializer.Deserialize<ErrorMessage>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "connection_error":
                            throw new ConnectionAbortedException();
                    }
                }

                Monitor.Pulse(lock1);
            }
        }

        private void Waiting() {
            lock (lock1) {
                waiting = true;
                while (waiting) {
                    Monitor.Wait(lock1);
                    waiting = false;
                }
            }
        }

        private void Send(object req) {
            string json = JsonSerializer.Serialize(req, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            byte[] data = Encoding.ASCII.GetBytes(json + "\n");
            networkStream.Write(data, 0, data.Length);
            Waiting();
            if (reply is ErrorMessage errorRequest)
                throw new Exception(errorRequest.Message);
        }
        
        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getAll",
                Index = index
            };
            Send(req);
            return ((ItemMessage)reply).Items;
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getCategories"
            };
            Send(req);
            return ((ItemMessage) reply).Categories;
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getGenres"
            };
            Send(req);
            return ((ItemMessage) reply).Genres;
        }

        public async Task<Item> AddItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "addItem",
                Item = item
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task<Book> AddBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "addBook",
                Book = book
            };
            Send(req);
            return ((ItemMessage) reply).Book;
        }

        public async Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getItemBySpecifications",
                Item = new Item() {
                    Name = name,
                    Description = description,
                    Category = category
                }
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task<Book> GetBookBySpecificationsAsync(string isbn) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getBookBySpecifications",
                Book = new Book() {
                    Isbn = isbn
                }
            };
            Send(req);
            return ((ItemMessage) reply).Book;
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemIds) {
            ItemMessage req = new ItemMessage() {
                Service = "item",
                Type = "getAllById",
                ItemsIds = itemIds
            };
            Send(req);
            return ((ItemMessage) reply).Items;
        }

        public async Task<Item> GetItemAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "get",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            Send(req);
           return ((ItemMessage) reply).Item;
        }

        public async Task<Book> GetBookAsync(int id) {
            ItemMessage req = new ItemMessage() {
                Type = "book",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            Send(req);
           return ((ItemMessage) reply).Book;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            OrderMessage req = new OrderMessage() {
                Service = "order", 
                Type = "purchase", 
                Order = order
            };
            Send(req);
            return ((OrderMessage)reply).Order;
        }

        public async Task<IList<Order>> GetOrdersAsync(int index, int id, string status) {
            OrderMessage req = new OrderMessage() {
                Service = "order",
                Type = "getAll",
                Index = index,
                CustomerId = id,
                Status = status
            };
            Send(req);
            return ((OrderMessage) reply).Orders;
        }

        public async Task<Order> GetOrderAsync(int orderId) {
            OrderMessage request = new OrderMessage() {
                Service = "order",
                Type = "get",
                Order = new Order() {
                    Id = orderId
                }
            };
            Send(request);
            return ((OrderMessage) reply).Order;
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
            Send(req);
            return ((CustomerMessage)reply).Customer;
        }
        
        public async Task<Customer> GetCustomerAsync(int customerId) {
            CustomerMessage req = new CustomerMessage() {
                Type = "get",
                Service = "customer",
                Customer = new Customer() {
                    Id = customerId
                }
            };
            Send(req);
            return ((CustomerMessage) reply).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerMessage req = new CustomerMessage() {
                Type = "register",
                Service = "customer",
                Customer = customer
            };
            Send(req);
            return ((CustomerMessage)reply).Customer;
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
            Send(req);
            return ((CustomerMessage) reply).Customer;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getWishlist",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemMessage) reply).Items;
        }

        public async Task<Item> AddToWishlist(int customerId, int itemId) {
            ItemMessage req = new ItemMessage() {
                Type = "addWishlist",
                Service = "item",
                Customer = new Customer() {Id = customerId},
                Item = new Item() {Id = itemId}
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task RemoveWishlistedItemAsync(Customer customer, Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "removeWishlist",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "addShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task<IList<Item>> GetShoppingCartAsync(Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "getShoppingCart",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemMessage) reply).Items;
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "editShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task RemoveFromShoppingCartAsync(Item item, Customer customer) {
            ItemMessage req = new ItemMessage() {
                Type = "removeShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
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
            Send(req);
            return ((CustomerMessage) reply).Notifications;
        }

        public async Task<IList<Customer>> GetAdminsAsync() {
            CustomerMessage req = new CustomerMessage() {
                Type = "getAdmins",
                Service = "customer"
            };
            Send(req);
            return ((CustomerMessage) reply).Customers;
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
            Send(req);
            return ((CustomerMessage) reply).Notification;
        }

        public async Task SendNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "sendNotification",
                Service = "customer",
                Customer = customer,
                Notification = notification
            };
            Send(req);
        }

        public async Task<Notification> UpdateSeenNotificationAsync(Customer customer, Notification notification) {
            CustomerMessage req = new CustomerMessage() {
                Type = "updateSeenNotification",
                Service = "customer",
                Customer = customer,
                Notification = notification
            };
            Send(req);
            return ((CustomerMessage) reply).Notification;
        }

        public async Task<IList<Customer>> GetCustomersByIndexAsync(int index) {
            CustomerMessage req = new CustomerMessage() {
                Type = "getCustomersByIndex",
                Service = "customer",
                Index = index
            };
            Send(req);
            return ((CustomerMessage) reply).Customers;
        }

        public async Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index) {
            ItemMessage req = new ItemMessage() {
                Type = "searchByName",
                Service = "item",
                Item = new Item {
                    Name = searchName
                },
                Index = index
            };
            Send(req);
            return ((ItemMessage)reply).Items;
        }

        public async Task<IList<Item>> GetItemsByCategoryAsync(string category, int index) {
            ItemMessage message = new ItemMessage() {
                Type = "getAllByCategory",
                Service = "item",
                Item = new Item() {
                    Name = category
                },
                Index = index
            };
            Send(message);
            return ((ItemMessage)reply).Items;
        }

        public async Task<Item> UpdateItemAsync(Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "updateItem",
                Service = "item",
                Item = item
            };
            Send(req);
            return ((ItemMessage) reply).Item;
        }

        public async Task<Book> UpdateBookAsync(Book book) {
            ItemMessage req = new ItemMessage() {
                Type = "updateBook",
                Service = "item",
                Book = book
            };
            Send(req);
            return ((ItemMessage) reply).Book;
        }

        public async Task<Category> AddCategoryAsync(Category category) {
            ItemMessage req = new ItemMessage() {
                Type = "addCategory",
                Service = "item",
                Categories = new List<Category>()
            };
            req.Categories.Add(category);

            Send(req);
            return ((ItemMessage) reply).Categories[0];
        }

        public async Task<IList<Item>> GetItemsByPriceAsync(string orderBy, int index) {
            ItemMessage request = new ItemMessage() {
                Type = "getAllByPrice",
                Service = "item",
                OrderBy = orderBy,
                Index = index
            };
            Send(request);
            return ((ItemMessage)reply).Items;
        }

        public async Task<IList<Review>> GetItemReviewsAsync(int index,Item item) {
            ItemMessage req = new ItemMessage() {
                Type = "getItemReviews",
                Service = "item",
                Index = index,
                Item = item
            };
            Send(req);
            return ((ItemMessage) reply).Reviews;
        }

        public async Task<Review> AddReviewAsync(Review review) {
            ItemMessage req = new ItemMessage() {
                Type = "addReview",
                Service = "item",
                Reviews = new List<Review>()
            };
            req.Reviews.Add(review);
            Send(req);
            return ((ItemMessage) reply).Reviews[0];
        }

        public async Task<IList<Order>> GetOrdersByCustomerAsync(int customerId, int index) {
            OrderMessage request = new OrderMessage() {
                Type = "getAllByCustomer",
                Service = "order",
                CustomerId = customerId,
                Index = index
            };
            Send(request);
            return ((OrderMessage) reply).Orders;
        }

        public async Task UpdateOrderItemsAsync(Order order) {
            OrderMessage request = new OrderMessage() {
                Type = "returnItems",
                Service = "order",
                Order = order
            };
            Send(request);
        }

        public async Task<Order> UpdateOrderAsync(Order order) {
            OrderMessage request = new OrderMessage() {
                Type = "update",
                Service = "order",
                Order = order
            };
            Send(request);
            return ((OrderMessage)reply).Order;
        }
        
        public async Task<IList<FAQ>> GetFrequentlyAskedQuestionsAsync() {
            Message request = new FAQMessage() {
                Type = "getAll",
                Service = "faq"
            };
            Send(request);
            return ((FAQMessage) reply).FAQs;
        }
        
        public async Task<FAQ> GetFrequentlyAskedQuestionAsync(int id) {
            Message request = new FAQMessage() {
                Type = "get",
                Service = "faq",
                Id = id
            };
            Send(request);
            return ((FAQMessage) reply).FAQ;
        }
        
        public async Task<FAQ> AddFrequentlyAskedQuestionAsync(FAQ faq) {
            Message request = new FAQMessage() {
                Type = "add",
                Service = "faq",
                FAQ = faq
            };
            Send(request);
            return ((FAQMessage) reply).FAQ;
        }
        
        public async Task DeleteFrequentlyAskedQuestionAsync(int id) {
            Message request = new FAQMessage() {
                Type = "delete",
                Service = "faq",
                Id = id
            };
            Send(request);
        }
    }
}