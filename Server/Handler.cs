using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Handler
    {
        private static readonly int PORT = 5050; // 포트

        private static readonly ILog log = LogManager.GetLogger(typeof(Handler)); // 로그

        public static Dictionary<int, User> User = new Dictionary<int, User>(); // 유저
        public static List<TcpClient> Client = new List<TcpClient>(); // 클라이언트 소켓 (필요할 지는 모르겠음...)

        private NetworkStream ns;
        private TcpClient client;
        private StreamReader sr;
        private StreamWriter sw;

        async public Task Listener()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), PORT);
            listener.Start();

            while (true)
            {
                log.Info("연결을 대기하는 중입니다...");
                client = await listener.AcceptTcpClientAsync(); // 클라이언트의 연결
                Client.Add(client);
                log.Info(Client);

                log.Info($"{client.Client.RemoteEndPoint} 의 연결을 수락했습니다.");
                ns = client.GetStream(); // 클라이언트의 스트림을 얻는다
                sr = new StreamReader(ns, Encoding.UTF8); // 스트림 읽기

                try
                {
                    string GetMessage;

                    while (client.Connected == true && !string.IsNullOrEmpty(sr.ReadLine()))
                    {
                        GetMessage = sr.ReadLine();
                        if (!string.IsNullOrEmpty(GetMessage)) HandleMessage(GetMessage);
                        Thread.Sleep(100);
                    }
                }
                catch (SocketException se)
                {
                    log.Error("소켓 에러발생", se);
                }
                catch (Exception e)
                {
                    log.Error("에러발생", e);
                }
                finally
                {
                    log.Info("클라이언트가 연결을 해제했습니다.");
                    sr.Close();
                    Client.Remove(client);
                    client.Close();
                    ns.Close();
                }
            }
        }

        public void HandleMessage(string message)
        {
            JObject receive = JObject.Parse(message);
            switch ((int)receive["type"])
            {
                case CTSHeader.MESSAGE:
                    log.Info(receive["text"].ToString()); break;
            }
        }

        public void SendMessage(dynamic message)
        {
            if (ns != null && message != null)
            {
                sw = new StreamWriter(ns, Encoding.UTF8); // 스트림 쓰기
                try
                {
                    sw.WriteLine(message); // 스트림에 메시지를 입력한다.
                    sw.Flush(); // 스트림 청소
                }
                catch (Exception e)
                {
                    log.Error("에러 발생", e);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

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
