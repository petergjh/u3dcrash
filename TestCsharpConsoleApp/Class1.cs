using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
​
public class Client : MonoBehaviour
{
    void Start()
    {
        CreateUDP();
    }
​
    bool isRunning;
    UdpClient udp;
    Thread thread;
    IPEndPoint ipendpoint;
    void CreateUDP()
    {
​
        udp = new UdpClient();
        ipendpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10086);
        udp.Connect(ipendpoint);
        isRunning = true;
        thread = new Thread(udpClientReceiveThread);
        thread.Start();
    }
​
    void udpClientReceiveThread()
    {

        while (isRunning)
        {
            try
            {
                byte[] bytes = udp.Receive(ref ipendpoint);
                string result = System.Text.Encoding.Default.GetString(bytes);
                Debug.Log(result);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
​
    public void SendMessageToServer()
    {
        string message = "Hello Server! I am Client:" + udp.Client.LocalEndPoint.ToString();
        byte[] bytes = System.Text.Encoding.Default.GetBytes(message);
        udp.Send(bytes, bytes.Length);
    }
​
    private void OnApplicationQuit()
    {
        isRunning = false;
        udp.Close();
        thread.Abort();
    }
}
