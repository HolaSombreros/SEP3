using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Microsoft.AspNetCore.Connections;
using SEP3Library.Model;
using SEP3WebAPI.Data;

namespace SEP3WebAPI.Mediator {
    public class Client : IRestService {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private IList<Item> items;
        private Order order;
        private Object lock1;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            lock1 = new Object();
        }

        public async Task ReceiveAsync(string result) {
            lock (lock1) {
                Request request = JsonSerializer.Deserialize<Request>(result, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if (request != null) {
                    Console.WriteLine(request.Type);
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

        // TODO - Could we do a Request<T> to avoid all the different variables in the "Request" class?
        // So for here T would be IList<Item> and for the method below, it would be Order. This way there would only be 1 variable in the "Request" class
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
            String send = JsonSerializer.Serialize(req, new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Console.WriteLine(send);
            byte[] data = Encoding.ASCII.GetBytes(send + "\n");
            networkStream.Write(data, 0, data.Length);
        }
    }
}