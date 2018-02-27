using Common;
using UnityEngine;

public class BaseRequest:MonoBehaviour
{
    protected GameFacade Facade { get; private set; }
    protected RequestCode _RequestCode = RequestCode.None;
    protected ActionCode _ActionCode = ActionCode.None;

    private string data;
    private bool exeResponse;

    private void Start()
    {
        Facade = GameFacade.Instance;
        Facade.AddRequest(_ActionCode, this);//todo,外观模式是否导致gamefacade过于复杂
        #region MyRegion
        //        ((RequestManger)GameFacade.Instance.GetManger(typeof(RequestManger))).AddRequest(requestCode,this);

        //        RequestManger requestManger = GameFacade.Instance.GetManger(typeof(RequestManger)) as RequestManger;
        //        if (requestManger!=null)
        //        {
        //            requestManger.AddRequest(requestCode,this);
        //        }
        //        else
        //        {
        //            Debug.Log("没有获取到RequestManger");
        //        }
        #endregion
    }

    private void Update()
    {
        if (exeResponse==false) return;

        OnResponse(data);
        exeResponse = false;
    }

    private void OnDestroy()
    {
        Facade.RemoveRequest(_ActionCode);//todo
        #region MyRegion
        //       ((RequestManger)GameFacade.Instance.GetManger(typeof(RequestManger))).RemoveRequest(requestCode);

        //        RequestManger requestManger = GameFacade.Instance.GetManger(typeof(RequestManger)) as RequestManger;
        //        if (requestManger != null)
        //        {
        //            requestManger.RemoveRequest(requestCode);
        //        }
        //        else
        //        {
        //            Debug.Log("没有获取到RequestManger");
        //        }
        #endregion
    }

    protected void SendRequest(string data)
    {
        if (Facade==null)
        {
            Facade = GameFacade.Instance;
        }
        Facade.SendRequest(_RequestCode,_ActionCode,data);
    }

    public void Response(string data)
    {
        this.data = data;
        exeResponse = true;
    }

    protected virtual void OnResponse(string data)
    {
        
    }
}
