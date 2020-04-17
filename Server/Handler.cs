﻿using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Handler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Handler)); // 로그

        private NetworkStream NS = null; // 네트워크 스트림
        private StreamReader SR = null; // 스트림 리더
        private StreamWriter SW = null; // 스트림 라이터
        private TcpClient client; // TCP 클라이언트

        public void StartClient(TcpClient clientSocket)
        {
            client = clientSocket;
            Thread echo_thread = new Thread(Echo);
            echo_thread.Start();
        }

        public void Echo()
        {
            log.Info("Start Thread [Echo()]");
            NS = client.GetStream();
            SR = new StreamReader(NS, Encoding.UTF8);
            SW = new StreamWriter(NS, Encoding.UTF8);

            string GetMessage;

            try
            {
                if (client.Connected == true)
                {
                    Connected(client);
                }
                while (client.Connected == true)
                {
                    GetMessage = SR.ReadLine();
                    if (GetMessage != null) HandleMessage(GetMessage);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error occured.", ex);
            }
            finally
            {
                SW.Close();
                SR.Close();
                client.Close();
                NS.Close();
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
            try
            {
                if (SW != null && message != null)
                {
                    SW.WriteLine(message);
                    SW.Flush();
                }
                else
                {
                    return;
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
