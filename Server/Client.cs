using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    /*
     * 미구현
     */
    class Client
    {
        TcpClient client;

        public string ID { get; private set; }
        public IPEndPoint EndPoint { get; private set; }

        public Client(TcpClient _client)
        {
            this.client = _client;
            this.ID = Guid.NewGuid().ToString();
            this.EndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        }

        public delegate void ClientDisconnectedHandler(Client client);
        public event ClientDisconnectedHandler Disconnected;
    }
}
