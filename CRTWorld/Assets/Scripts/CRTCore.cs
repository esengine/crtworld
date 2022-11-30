using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class CRTCore : MonoBehaviour
{
    Socket socket;
    Thread thread;
    Thread connectThread;

    void Start()
    {
        Application.targetFrameRate = 60;

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        connectThread = new Thread(ConnectServer);
        connectThread.IsBackground = true;
        connectThread.Start();
    }

    void ConnectServer(object obj) {
        var ip = IPAddress.Parse("127.0.0.1");
        var endpoint = new IPEndPoint(ip, Convert.ToInt32("9090"));

        while (!socket.Connected) {
            try {
                socket.Connect(endpoint);
            } catch {}
        }

        thread = new Thread(Receive);
        thread.IsBackground = true;
        thread.Start();
        connectThread.Join();
        connectThread.Abort();
    }

    void Receive(object obj) {
        var buffer = new byte[1024];
        while (true) {  
            var r = socket.Receive(buffer);
        }
    }
}
