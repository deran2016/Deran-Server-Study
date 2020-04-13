using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Server
{
    partial class Handler
    {
        public static Dictionary<int, User> User = new Dictionary<int, User>();
        public static Dictionary<int, Client> Client = new Dictionary<int, Client>();

        // 연결되었을 때
        public void Connected(TcpClient client)
        {
            log.Info($"Client connected: {client.Client.RemoteEndPoint.ToString()}");
            SendHello();
        }

        public void Disconnected(TcpClient client)
        {
            log.Info("Client Disconnected");
        }

        // Hello 메세지를 보낸다.
        public void SendHello()
        {
            dynamic send = new JObject();
            send.type = 1;
            send.text = "hello";
            SendMessage(send);
        }
    }
}
