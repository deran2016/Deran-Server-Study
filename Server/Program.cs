using log4net;
using System;
using System.Net;
using System.Net.Sockets;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace Server
{
    class Program
    {
        // Log4net
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // PORT
        private static readonly int PORT = 5050;

        static void Main(string[] args)
        {
            Console.Title = "Server"; // Title

            TcpListener Listener = null; // TcpListener
            TcpClient client = null; // TcpClient

            try
            {
                log.Info("서버 가동");

                Handler r = new Handler();
                log.Info("Handler 생성");

                Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), PORT);
                Listener.Start();

                while (true)
                {
                    log.Info("클라이언트 인식 루프 가동");
                    client = Listener.AcceptTcpClient();
                    log.Info("client 생성");
                    r.StartClient(client);
                }
            }
            catch (Exception e)
            {
                log.Error("Error occured", e);
            }
            finally
            {

            }
        }
    }
}
