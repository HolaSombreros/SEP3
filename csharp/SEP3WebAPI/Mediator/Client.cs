using System;

using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Microsoft.AspNetCore.Connections;
using SEP3WebAPI.Mediator.Messages;

namespace SEP3WebAPI.Mediator {
    public class Client : IClient {
        private TcpClient tcpClient;
        private int port = 1234;
        private NetworkStream networkStream;
        private bool waiting;
        private object lock1;
        private Message reply;

        public Client() {
            tcpClient = new TcpClient("127.0.0.1", port);
            networkStream = tcpClient.GetStream();
            ClientReceiver clientReceiver = new ClientReceiver(this, networkStream);
            clientReceiver.Run();
            lock1 = new object();
        }

        /**
         * The method is synchronized with a lock
         * First the message is serialized in a basic message and then to a specific one based on the service in a switch
         * The method also notifies the waiting thread 
         */
        public void Receive(string result) {
            lock (lock1) {
                reply = JsonSerializer.Deserialize<Message>(result,
                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if (reply != null) {
                    reply = reply.Service switch {
                        "item" => JsonSerializer.Deserialize<ItemMessage>(result,
                            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}),
                        "order" => JsonSerializer.Deserialize<OrderMessage>(result,
                            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}),
                        "customer" => JsonSerializer.Deserialize<CustomerMessage>(result,
                            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}),
                        "faq" => JsonSerializer.Deserialize<FAQMessage>(result,
                            new JsonSerializerOptions() {PropertyNameCaseInsensitive = true}),
                        "error" => JsonSerializer.Deserialize<ErrorMessage>(result,
                            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}),
                        "connection_error" => throw new ConnectionAbortedException(),
                        _ => reply
                    };
                }
                Monitor.Pulse(lock1);
            }
        }

        /**
         * The method is synchronized with a lock
         * The thread waits until it is notified and releases the lock
         */
        private void Waiting() {
            lock (lock1) {
                waiting = true;
                while (waiting) {
                    Monitor.Wait(lock1);
                    waiting = false;
                }
            }
        }

        /**
         * The method Serialize the request coming from the server and waits for the reply
         * An additional "/n" is added at the end in order to be compatible with java
         */
        public Message Send(object req) {
            string json = JsonSerializer.Serialize(req, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            byte[] data = Encoding.ASCII.GetBytes(json + "\n");
            networkStream.Write(data, 0, data.Length);
            Waiting();
            if (reply is ErrorMessage errorRequest)
                throw new Exception(errorRequest.Message);
            return reply;
        }

        public void Disconnect() {
            networkStream.Close();
            tcpClient.Close();
        }
    }
}