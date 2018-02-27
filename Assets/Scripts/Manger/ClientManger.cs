using System;
using System.Net.Sockets;
using Common;
using UnityEngine;

/// <summary>
/// 处理和服务器的socket连接
/// </summary>
public class ClientManger:BaseManger
{
    private const string Ip = "39.104.75.222";
    private const int Port = 6688;

    private Socket clientSocket;
    private Message msg=new Message();

    public ClientManger(GameFacade facade) : base(facade)
    {
    }

    public override void OnInit()
    {
        clientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);//创建通信的socket
        try
        {
            clientSocket.Connect(Ip, Port);
            StartReceive();
        }
        catch (Exception e)
        {
            Debug.Log("无法建立和服务器的连接："+e);
        }
    }

    public override void OnDestory()
    {
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.Log("无法关闭跟服务器的连接："+e);
        }
    }

    //向服务器发送请求
    public void SendRequest(RequestCode requestCode,ActionCode actionCode, string data)
    {
        byte[] bytes=Message.PackData(requestCode,actionCode,data);
        clientSocket.Send(bytes);
    }

    #region 异步接收消息
    private void StartReceive()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
    }
    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessCallback);
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("接收数据异常：" + e);
        }
    }

    #endregion
    
    //处理响应
    private void OnProcessCallback(ActionCode actionCode, string data)
    {
        Facade.HandleResponse(actionCode, data);
        #region MyRegion
        //        ((RequestManger)GameFacade.Instance.GetManger(typeof(RequestManger))).HandleResponse(requestCode, data);//todo

        //        RequestManger requestManger = GameFacade.Instance.GetManger(typeof(RequestManger)) as RequestManger;
        //        if (requestManger != null)
        //        {
        //            requestManger.HandleResponse(requestCode,data);
        //        }
        //        else
        //        {
        //            Debug.Log("没有从GameFacade中得到RequestManger");
        //        }
        #endregion
    }
}
