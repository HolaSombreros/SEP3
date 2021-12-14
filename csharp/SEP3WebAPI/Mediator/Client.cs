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

        private void Waiting() {
            lock (lock1) {
                waiting = true;
                while (waiting) {
                    Monitor.Wait(lock1);
                    waiting = false;
                }
            }
        }

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