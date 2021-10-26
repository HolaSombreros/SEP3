using System.Net.Sockets;
using System.Threading;

namespace SEP3WebAPI.Mediator {
    public class ClientReceiver {
        private TcpClient client;
        private NetworkStream stream;
        private Thread thread;

        public ClientReceiver() {
            client = new TcpClient("10.154.206.142", 1234);
            stream = client.GetStream();
        }
    }
}