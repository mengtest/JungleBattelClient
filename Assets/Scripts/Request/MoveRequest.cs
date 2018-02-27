using Common;
using UnityEngine;
using ZJD;

public class MoveRequest:BaseRequest
{
    private Transform localPlayerTs;
    private PlayerMove localPlayerMove;

    private Transform remotePlayerTs;
    private Animator remotePlayerAni;

    private int syncRate = 20;

    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.Move;
    }

    //设置本地角色数据
    public void SetLocalPlayer(Transform player, PlayerMove move)
    {
        localPlayerTs = player;
        localPlayerMove = move;
    }

    //设置同步角色数据
    public void SetRemotePlayer(Transform player, Animator ani)
    {
        remotePlayerTs = player;
        remotePlayerAni = ani;
    }

    //开始位置同步请求
    public void StartSync()
    {
        InvokeRepeating("SyncLocalPlayer", 0.1f, 1f / syncRate);
    }

    //发起同步请求
    private void SyncLocalPlayer()
    {
        SendMoveRequest(localPlayerTs.position, localPlayerTs.eulerAngles, localPlayerMove.Forward);
    }

    private void SendMoveRequest(Vector3 pos,Vector3 rot,float forward)
    {
        string data = string.Format("{0},{1},{2}|{3},{4},{5}|{6}",pos.x,pos.y,pos.z,rot.x,rot.y,rot.z,forward);
        base.SendRequest(data);
    }

    //接收同步响应
    protected override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        Vector3 pos = Vct3Ex.Parse(strs[0]);
        Vector3 rot = Vct3Ex.Parse(strs[1]);
        float forward = float.Parse(strs[2]);
        SyncRemotePlayer(pos,rot,forward);
    }

    //同步远程player的位置
    private void SyncRemotePlayer(Vector3 pos,Vector3 rot,float foward)
    {
        remotePlayerTs.position = pos;
        remotePlayerTs.eulerAngles = rot;
        remotePlayerAni.SetFloat(MyAnimator.Float.Forward,foward);
    }
}
