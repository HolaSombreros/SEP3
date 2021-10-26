using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using SEP3UI.Model;

namespace SEP3WebAPI.Mediator {
    public class Client {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private IList<Item> items;

        public Client() {
            tcpClient = new TcpClient("10.154.206.142", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
        }

        public async Task Receive(string result) {
            Request request = JsonSerializer.Deserialize<Request>(result, new JsonSerializerOptions {WriteIndented = true, PropertyNameCaseInsensitive = false});
            switch (request.Type) {
                case "items":
                    items = request.Items;
                    break;
                case "error":
                    items = null;
                    break; 
                case "connection_error":
                    throw new ConnectionAbortedException();
            }
        }

        // TODO methods to call

        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }
    }
}