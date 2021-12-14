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
        }

        /**
         * The method runs a thread that will run in a while and read messages from the server
         * First the method handle the amount of data that will be sent and set the lenght and then it will handle the message
         * The extra "/n" is replaced with an empty string
         * The message is sent back to the client
         */
        public void Run() {
            thread = new Thread(() => {
                while (running) {
                    try {
                        byte[] response = new byte[16];
                        int bytesRead = stream.Read(response, 0, response.Length);
                        string result = Encoding.ASCII.GetString(response, 0, bytesRead);
                        result = result.Replace("\n", "");
                        
                        response = new byte[int.Parse(result) + 2];
                        bytesRead = stream.Read(response, 0, response.Length);
                        
                        result = Encoding.ASCII.GetString(response, 0, bytesRead);
                        result = result.Replace("\n", "");
                        client.Receive(result);
                    } catch (ConnectionAbortedException e) {
                        Console.WriteLine(e.Message);
                        Disconnect();
                    } catch (Exception e) {
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