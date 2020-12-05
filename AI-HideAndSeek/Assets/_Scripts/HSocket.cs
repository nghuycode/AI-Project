using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSocket : MonoBehaviour
{
    public Socket socket_server = null;
    private void Start() {
        socket_server = CreateServer(Dns.GetHostName(), 8000);
    }
    private static Socket CreateServer(string server, int port)
    {
        IPHostEntry hostEntry = Dns.GetHostEntry(server);
        IPAddress address = hostEntry.AddressList[2];
        IPEndPoint ipe = new IPEndPoint(address, port);
        Socket socket_server = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket_server.Bind(ipe);
        socket_server.Listen(10);
        return socket_server;
    }

    static string Receive(Socket socket)
    {
        Byte[] bytesReceived = new Byte[256];
        string page = "";
        {
            int bytes = 0;
            do
            {
                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);
        }
        return page;
    }

    public void Socket()
    {
        StartCoroutine(RunSocket());
    }
    private IEnumerator RunSocket() {
        string message = "";
        Socket socket = socket_server.Accept();
        message = Receive(socket);
        yield return new WaitUntil(() => message != "");
        Debug.Log(message);
        IOCommunicate.Instance.Load(message);
    }
}
