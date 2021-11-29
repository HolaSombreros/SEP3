using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using SEP3Library.Models;
using SEP3WebAPI.Mediator.Requests;

namespace SEP3WebAPI.Mediator {
    public class Client : IClient {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private object lock1;
        private Request reply;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new object();
        }

        public void Receive(string result) {
            lock (lock1) {
                reply = JsonSerializer.Deserialize<Request>(result, 
                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if (reply != null) {
                    switch (reply.Service) {
                        case "item":
                            reply = JsonSerializer.Deserialize<ItemRequest>(result, 
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "order":
                            reply = JsonSerializer.Deserialize<OrderRequest>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "customer":
                            reply = JsonSerializer.Deserialize<CustomerRequest>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "error":
                            reply = JsonSerializer.Deserialize<ErrorRequest>(result,
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
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            byte[] data = Encoding.ASCII.GetBytes(json + "\n");
            networkStream.Write(data, 0, data.Length);
            Waiting();
            if (reply is ErrorRequest errorRequest)
                throw new Exception(errorRequest.Message);
        }
        
        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }
        
        public async Task<IList<Item>> GetItemsAsync(int index) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getAll",
                Index = index
            };
            Send(req);
            return ((ItemRequest)reply).Items;
        }

        public async Task<IList<Category>> GetCategoriesAsync() {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getCategories"
            };
            Send(req);
            return ((ItemRequest) reply).Categories;
        }

        public async Task<IList<Genre>> GetGenresAsync() {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getGenres"
            };
            Send(req);
            return ((ItemRequest) reply).Genres;
        }

        public async Task<Item> AddItemAsync(Item item) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "addItem",
                Item = item
            };
            Send(req);
            return ((ItemRequest) reply).Item;
        }

        public async Task<Book> AddBookAsync(Book book) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "addBook",
                Book = book
            };
            Send(req);
            return ((ItemRequest) reply).Book;
        }

        public async Task<Item> GetItemBySpecificationsAsync(string name, string description, Category category) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getItemBySpecifications",
                Item = new Item() {
                    Name = name,
                    Description = description,
                    Category = category
                }
            };
            Send(req);
            return ((ItemRequest) reply).Item;
        }

        public async Task<Book> GetBookBySpecificationsAsync(string isbn) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getBookBySpecifications",
                Book = new Book() {
                    Isbn = isbn
                }
            };
            Send(req);
            return ((ItemRequest) reply).Book;
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemIds) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getAllById",
                ItemsIds = itemIds
            };
            Send(req);
            return ((ItemRequest) reply).Items;
        }

        public async Task<Item> GetItemAsync(int id) {
            ItemRequest req = new ItemRequest() {
                Type = "get",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            Send(req);
           return ((ItemRequest) reply).Item;
        }

        public async Task<Book> GetBookAsync(int id) {
            ItemRequest req = new ItemRequest() {
                Type = "book",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            Send(req);
           return ((ItemRequest) reply).Book;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            // TODO json too long problem
            OrderRequest req = new OrderRequest() {
                Service = "order", 
                Type = "purchase", 
                Order = order
            };
            Send(req);
            return ((OrderRequest)reply).Order;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            CustomerRequest req = new CustomerRequest() {
                Type = "login",
                Service = "customer",
                Customer = new Customer() {
                    Email = email,
                    Password = password
                }
            };
            Send(req);
            return ((CustomerRequest)reply).Customer;
        }
        
        public async Task<Customer> GetCustomerAsync(int customerId) {
            CustomerRequest req = new CustomerRequest() {
                Type = "get",
                Service = "customer",
                Customer = new Customer() {
                    Id = customerId
                }
            };
            Send(req);
            return ((CustomerRequest) reply).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerRequest req = new CustomerRequest() {
                Type = "register",
                Service = "customer",
                Customer = customer
            };
            Send(req);
            return ((CustomerRequest)reply).Customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer) {
            CustomerRequest req = new CustomerRequest() {
                Type = "update",
                Service = "customer",
                Customer = customer
            };
            Send(req);
            return ((CustomerRequest) reply).Customer;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(Customer customer) {
            // TODO json too long problem
            ItemRequest req = new ItemRequest() {
                Type = "getWishlist",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemRequest) reply).Items;
        }

        public async Task<Item> AddToWishlist(int customerId, int itemId) {
            ItemRequest req = new ItemRequest() {
                Type = "addWishlist",
                Service = "item",
                Customer = new Customer() {Id = customerId},
                Item = new Item() {Id = itemId}
            };
            Send(req);
            return ((ItemRequest) reply).Item;
        }

        public async Task RemoveWishlistedItemAsync(Customer customer, Item item) {
            ItemRequest req = new ItemRequest() {
                Type = "removeWishlist",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
        }

        public async Task<Item> AddToShoppingCartAsync(Item item, Customer customer) {
            Console.WriteLine("client");
            ItemRequest req = new ItemRequest() {
                Type = "addShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
            return ((ItemRequest) reply).Item;
        }

        public async Task<IList<Item>> GetShoppingCartAsync(Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "getShoppingCart",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemRequest) reply).Items;
        }

        public async Task<Item> UpdateShoppingCartAsync(Item item, Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "editShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
            return ((ItemRequest) reply).Item;
        }

        public async Task RemoveFromShoppingCartAsync(Item item, Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "removeShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
        }

        public async Task<IList<Item>> GetItemsBySearchAsync(string searchName, int index) {
            ItemRequest req = new ItemRequest() {
                Type = "searchByName",
                Service = "item",
                Item = new Item {
                    Name = searchName
                },
                Index = index
            };
            Send(req);
            Waiting();
            return ((ItemRequest)reply).Items;
        }
    }
}