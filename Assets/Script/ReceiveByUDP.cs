using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ReceiveByUDP : MonoBehaviour
{
    public static ReceiveByUDP _instance;

    private Thread thread;

    private UdpClient client;

    // 这个表示服务器UDP的端口号
    public int port = 9090;
    public bool startReceiveing = true;
    public string data;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        thread = new Thread(new ThreadStart(ReceiveData));
        thread.IsBackground = true;
        thread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiveing)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(bytes);
                Debug.Log("收到信息");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}