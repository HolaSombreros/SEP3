using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using SEP3Library.Model;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Mediator {
    public class Client : IClient {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private Object lock1;
        private Request request;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new Object();
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
                            throw new Exception(((ErrorRequest)request)?.Message);
                        case "connection_error":
                            throw new ConnectionAbortedException();
                    }
                }
                Monitor.Pulse(lock1);
            }
        }

        public void Waiting() {
            lock (lock1) {
                waiting = true;
                while (waiting) {
                    Monitor.Wait(lock1);
                    waiting = false;
                }
            }
        }

        public async Task<IList<Item>> GetItemsAsync() {
            ItemRequest req = new ItemRequest() {
                Service = "item",
                Type = "getAll"
            };
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Send(send);
            Waiting();
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
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Send(send);
            Waiting();
            return ((ItemRequest) request).Item;
        }

        public async Task<Book> GetBookAsync(int id) {
            ItemRequest req = new ItemRequest() {
                Type = "get",
                Service = "item",
                Item = new Item() {
                    Id = id
                }
            };
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Send(send);
            Waiting();
            return ((ItemRequest) request).Book;
        }
        
        public async Task<Order> CreateOrderAsync(Order order) {
            OrderRequest req = new OrderRequest() {
                Service = "order", 
                Type = "purchase", 
                Order = order
            };
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Send(send);
            Waiting();
            return ((OrderRequest)request).Order;
        }
        
        public async Task<Customer> GetCustomerAsync(string email, string password) {
            Request req = new Request();
            req.Type = "login";
            req.Customer.Email = email;
            req.Customer.Password = password;
            Send(req);
            Waiting();
            return customer;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer) {
            Request req = new Request();
            req.Type = "register";
            req.Customer.FirstName = customer.FirstName;
            req.Customer.LastName = customer.LastName;
            req.Customer.Password = customer.Password;
            req.Customer.Address = customer.Address;
            req.Customer.Email = customer.Email;
            Send(req);
            Waiting();
            return this.customer;
        }

        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }

        private void Send(String send) {
            byte[] data = Encoding.ASCII.GetBytes(send + "\n");
            networkStream.Write(data, 0, data.Length);
        }
    }
}