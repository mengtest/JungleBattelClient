using Common;
using UnityEngine;

public class ShootRequest : BaseRequest
{
    private GameObject remoteArrowGo;

    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.Shoot;
    }

    //设置需要同步（实例化）的箭的预制体
    public void SetRemoteArrow(GameObject go)
    {
        remoteArrowGo = go;
    }

    public void SendShootRequest( Vector3 p,Vector3 e)
    {
        string data=string.Format("{0},{1},{2}|{3},{4},{5}",p.x,p.y,p.z,e.x,e.y,e.z);
        base.SendRequest(data);
    }

    protected override void OnResponse(string data)
    {
        string[] res = data.Split('|');
        Vector3 pos = Vct3Ex.Parse(res[0]);
        Vector3 eul = Vct3Ex.Parse(res[1]);

        Instantiate(remoteArrowGo, pos, Quaternion.Euler(eul));
        Facade.PlayNorSound(AudioManger.Sound_Timer);
    }
}
