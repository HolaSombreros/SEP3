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
    public class Client : IModelService {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private IList<Item> items;
        private Order order;
        private Object wait;

        public Client() {
            tcpClient = new TcpClient("10.154.206.142", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            wait = new Object();
        }

        public async Task ReceiveAsync(string result) {
            Object obj = new Object();
            lock (obj) {
                Request request = JsonSerializer.Deserialize<Request>(result,
                    new JsonSerializerOptions {WriteIndented = true, PropertyNameCaseInsensitive = false});
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
                Monitor.Exit(wait);
            }
        }

        public void Waiting() {
            Object obj = new Object();
            lock (obj) {
                waiting = true;
                while (waiting) {
                    Monitor.Enter(wait);
                    waiting = false;
                }
            }
        }

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