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

        public Client(TcpClient _client)
        {
            client = _client;
        }

        public void Close()
        {
            client.Dispose();
            client.Close();
        }
    }
}
