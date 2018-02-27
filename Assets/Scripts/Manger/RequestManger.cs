using System.Collections.Generic;
using Common;
using UnityEngine;
using ZJD;

//RequestManger管理的BaseRequest由Unity自己实例化
public class RequestManger : BaseManger
{
    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

//    public override void OnInit()
//    {
//        base.OnInit();
//        //todo,最好在此处添加所有的Request
//        //两种注入方式的对比
//        //BaseRequest自动添加到RequestManger中
//        //RequestMange来创建BaseRequest
//    }

    public RequestManger(GameFacade facade) : base(facade)
    {

    }

    //request继承自MonoBehaviour,通过调用管理类的Add方法进行管理
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestDict.Add(actionCode, request);
    }

    //request继承自MonoBehaviour,通过调用管理类的Remove方法进行删除
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    //处理服务器响应
    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest request = requestDict.TryGet(actionCode);
        if (request!=null)
        {
            request.Response(data);
        }
        else
        {
            Debug.Log(string.Format("无法得到【{0}】对应的Request类", actionCode));
        }
    }
}
