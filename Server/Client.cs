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
            client = _client;
            ID = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        }

        public void Close()
        {
            client.Dispose();
            client.Close();
        }

        public delegate void ClientDisconnectedHandler(Client client);
        public event ClientDisconnectedHandler Disconnected;
    }
}
