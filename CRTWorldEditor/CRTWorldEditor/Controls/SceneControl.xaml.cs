using CRTWorldEditor.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Panel = System.Windows.Forms.Panel;
using System.Text;

namespace CRTWorldEditor.Controls
{
    /// <summary>
    /// SceneControl.xaml 的交互逻辑
    /// </summary>
    public partial class SceneControl : UserControl
    {
        private AppContainer container;
        private Socket serverSocket;
        private Socket clientSocket;
        private Thread myThread;
        private Thread receiveThread;

        public SceneControl()
        {
            InitializeComponent();

            var unityHost = new Panel();
            Host.Child = unityHost;

            var exePath = Path.Combine(Environment.CurrentDirectory, "unity", "CRTWorld.exe");
            if (!File.Exists(exePath) )
                return;

            StartServer();

            container = new AppContainer(unityHost);
            container.StartAndEmbedProcess(exePath);
        }

        private void StartServer() {
            var ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, 9090));
            serverSocket.Listen(10);
            myThread = new Thread(ListenClientConnect);
            myThread.IsBackground = true;
            myThread.Start();
        }

        private void ListenClientConnect()
        {
            while (true)
            {
                try
                {
                    clientSocket = serverSocket.Accept();
                    //string clientInfo = clientSocket.RemoteEndPoint.ToString();
                    receiveThread = new Thread(ReceiveMessage);
                    receiveThread.IsBackground = true;
                    receiveThread.Start(clientSocket);
                }
                catch (Exception)
                {

                }
            }
        }

        private void ReceiveMessage()
        {
            Socket myClientSocket = (Socket)clientSocket;
            var buffer = new byte[1024];
            while (true)
            {
                var receiveNumber = myClientSocket.Receive(buffer);
            }
        }
        internal void SendMessage(string msg)
        {
            clientSocket.Send(Encoding.ASCII.GetBytes(msg));
        }
    }
}
