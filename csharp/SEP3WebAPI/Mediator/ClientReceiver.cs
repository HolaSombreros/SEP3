using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Connections;

namespace SEP3WebAPI.Mediator {
    public class ClientReceiver {
        private Client client;
        private NetworkStream stream;
        private Thread thread;
        private bool running;

        public ClientReceiver(Client client, NetworkStream networkStream) {
            this.client = client;
            stream = networkStream;
            running = true;
            Run();
        }

        private void Run() {
            thread = new Thread(() => {
                while (running) {
                    try {
                        byte[] response = new byte[1024];
                        int bytesRead = stream.Read(response, 0, response.Length);
                        string result = Encoding.ASCII.GetString(response, 0, bytesRead);
                        result = result.Replace("\n", "");
                        client.ReceiveAsync(result);
                    }
                    catch (ConnectionAbortedException e) {
                        Disconnect();
                    }
                }
            });
            thread.Start();
        }

        public void Disconnect() {
            running = false;
            stream.Close();
            client.Disconnect();
        }
    }
}