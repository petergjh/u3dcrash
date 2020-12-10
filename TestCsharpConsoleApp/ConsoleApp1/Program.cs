using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
​
public class Server : MonoBehaviour
{
    void Start()
    {
        StartServer(10086);
    }
​
​
    UdpClient udpClient;
    IPEndPoint sendipendpoint;
    Thread updReceiveThread;
​
​
    List<int> clientPortList = new List<int>();
    List<IPEndPoint> clients = new List<IPEndPoint>();

    // 创建一个IP地址和端口的组合，用来拿到发送消息过来的客户端的IP地址以及端口
    // 当真正接收到客户端的消息时，这个IP地址和端口的组合会改变为客户端的IP地址和端口组合
    IPEndPoint receiveEndPoint;

    // 是否需要接收客户端的消息（线程中用于终止接收消息，防止崩溃）
    bool isRunning;
    void StartServer(int port)
    {

        // 创建一个UdpClient,并绑定一个端口用于接收客户端的消息
        udpClient = new UdpClient(port);

        isRunning = true;

        // 开启线程用于接收客户端的消息
        updReceiveThread = new Thread(ReceiveMessage);
        updReceiveThread.Start();
    }

    void ReceiveMessage()
    {

        while (isRunning)
        {
            try
            {
                if (udpClient.Client.Available > 0)
                {
                    byte[] bytes = udpClient.Receive(ref receiveEndPoint);

                    if (!clientPortList.Contains(receiveEndPoint.Port))
                    {
                        clients.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), receiveEndPoint.Port));
                    }

                    Debug.Log(Encoding.Default.GetString(bytes));
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }

    public void SendMessageToClient()
    {
        string message = "Hello Client!";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(message);

        for (int i = 0; i < clients.Count; i++)
        {
            udpClient.Send(bytes, bytes.Length, clients[i]);
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        isRunning = false;
        udpClient.Close();
        updReceiveThread.Abort();
    }
}

//注意:
//因为子线程是用while进行侦测接收，当终止接收的时候注意处理相应的收尾（UdpClient.Close, while条件），防止死循环崩溃。