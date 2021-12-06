using System;
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
                        // Handle amount of data that will be sent
                        byte[] response = new byte[16];
                        int bytesRead = stream.Read(response, 0, response.Length);
                        string result = Encoding.ASCII.GetString(response, 0, bytesRead);
                        result = result.Replace("\n", "");
                        Console.WriteLine(">>>>> Length: " + result);
                        Console.WriteLine(result);
                        
                        // Handle the actual data
                        response = new byte[int.Parse(result) + 2];
                        Console.WriteLine(">>>>> Allocated: " + response.Length);
                        bytesRead = stream.Read(response, 0, response.Length);
                        
                        result = Encoding.ASCII.GetString(response, 0, bytesRead);
                        result = result.Replace("\n", "");
                        Console.WriteLine(">>>>> Actual: " + result.Length);
                        Console.WriteLine(result);
                        client.Receive(result);
                    }
                    catch (ConnectionAbortedException e) {
                        Console.WriteLine(e.Message);
                        Disconnect();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            });
            thread.Start();
        }

        private void Disconnect() {
            running = false;
            stream.Close();
            client.Disconnect();
        }
    }
}