using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using SEP3UI.Model;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Mediator {
    public class Client : IModelService {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private IList<Item> items;
        private Order order;
        private Object lock1;
        private Object lock2;

        public Client() {
            tcpClient = new TcpClient("10.154.206.142", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new Object();
            lock2 = new Object();
        }

        // This method is called by the ClientReceiver class which listens continuously to the server
        // lock1 and lock2 are just simple objects to use for synchronization
        // The method saves the information in a local variable depending on the request type
        public async Task ReceiveAsync(string result) {
            lock (lock1) {
                Request request = JsonSerializer.Deserialize<Request>(result,
                    new JsonSerializerOptions {WriteIndented = true, PropertyNameCaseInsensitive = false});
                if (request != null) {
                    switch (request.Type) {
                        case "items":
                            items = request.Items;
                            break;
                        case "purchase":
                            order = request.Order;
                            break;
                        case "error":
                            items = null;
                            order = null;
                            break;
                        case "connection_error":
                            throw new ConnectionAbortedException();
                    }
                }
                Monitor.Exit(lock2);
            }
        }

        // This method uses lock1 for synchronization
        // This method is called when a request is waiting for a reply
        public void Waiting() {
            lock (lock1) {
                waiting = true;
                while (waiting) {
                    Monitor.Enter(lock2);
                    waiting = false;
                }
            }
        }

        // This method sends a request for the list of items
        // The method waits for the reply (which will come through the ClientReceiver and ReceiveAsync method)
        // The method returns the list saved as a variable in this class
        public async Task<IList<Item>> GetItemsAsync() {
            Request req = new Request();
            req.Type = "items";
            Send(req);
            Waiting();
            return items;
        }

        public async Task<Order> CreateOrderAsync(Order order) {
            Request req = new Request();
            req.Type = "purchase";
            req.Order = order;
            Send(req);
            Waiting();
            return this.order;
        }

        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }

        public void Send(Request req) {
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {WriteIndented = true});
            byte[] data = Encoding.ASCII.GetBytes(send);
            networkStream.Write(data, 0, data.Length);
        }
    }
}