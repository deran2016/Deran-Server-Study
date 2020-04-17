using log4net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace Server
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program)); // 로그

        static void Main(string[] args)
        {
            Console.Title = "Server"; // 콘솔 제목

            log.Info("서버를 가동합니다.");

            Handler r = new Handler(); // 핸들러
            r.Listener(); // 리스너 실행
        }
    }
}
