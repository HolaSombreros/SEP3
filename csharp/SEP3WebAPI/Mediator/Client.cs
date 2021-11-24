﻿using System;
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
        private Request request;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new object();
        }

        public void Receive(string result) {
            lock (lock1) {
                request = JsonSerializer.Deserialize<Request>(result, 
                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if (request != null) {
                    switch (request.Service) {
                        case "item":
                            request = JsonSerializer.Deserialize<ItemRequest>(result, 
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "order":
                            request = JsonSerializer.Deserialize<OrderRequest>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "customer":
                            request = JsonSerializer.Deserialize<CustomerRequest>(result,
                                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                            break;
                        case "error":
                            request = JsonSerializer.Deserialize<ErrorRequest>(result,
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
            if (request is ErrorRequest errorRequest)
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
                Index = index,
            };
            Send(req);
            return ((ItemRequest)request).Items;
        }

        public async Task<IList<Item>> GetItemsByIdAsync(int[] itemIds) {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getAllById",
                ItemsIds = itemIds
            };
            Send(req);
            return ((ItemRequest)request).Items;
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
           return ((ItemRequest) request).Item;
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
           return ((ItemRequest) request).Book;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            // TODO json too long problem
            OrderRequest req = new OrderRequest() {
                Service = "order", 
                Type = "purchase", 
                Order = order
            };
            Send(req);
            return ((OrderRequest)request).Order;
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
            return ((CustomerRequest)request).Customer;
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
            return ((CustomerRequest) request).Customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            CustomerRequest req = new CustomerRequest() {
                Type = "register",
                Service = "customer",
                Customer = new Customer() {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Password = customer.Password,
                    Address = customer.Address,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Role = customer.Role
                }
            };
            Send(req);
            return ((CustomerRequest)request).Customer;
        }

        public async Task<IList<Item>> GetCustomerWishlistAsync(Customer customer) {
            // TODO json too long problem
            ItemRequest req = new ItemRequest() {
                Type = "getWishlist",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemRequest) request).Items;
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

        public async Task AddToShoppingCartAsync(Item item, Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "addShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
        }

        public async Task<IList<Item>> GetShoppingCartAsync(Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "getShoppingCart",
                Service = "item",
                Customer = customer
            };
            Send(req);
            return ((ItemRequest) request).Items;
        }

        public async Task UpdateShoppingCartAsync(Item item, Customer customer) {
            ItemRequest req = new ItemRequest() {
                Type = "editShoppingCart",
                Service = "item",
                Customer = customer,
                Item = item
            };
            Send(req);
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
    }
}