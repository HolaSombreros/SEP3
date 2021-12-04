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
                        Console.WriteLine(">>>>> Length: " + result);
                        result = result.Replace("\n", "");
                        Console.WriteLine(result);
                        
                        // Handle the actual data
                        response = new byte[int.Parse(result) + 4];
                        Console.WriteLine(">>>>> Allocated: " + response.Length);
                        bytesRead = stream.Read(response, 0, response.Length);
                        
                        // Using UTF8 allows for special characters like Æ Ø and Å
                        result = Encoding.UTF8.GetString(response, 0, bytesRead);
                        Console.WriteLine(">>>>> Actual: " + result.Length);
                        result = result.Replace("\n", "");
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