using System.Collections.Generic;
using Common;
using UnityEngine;

public class PlayManger : BaseManger
{
    //账户数据
    public UserData UserData { get; set; }

    //所有的角色数据
    private Dictionary<RoleType, RoleData> roleDateDict = new Dictionary<RoleType, RoleData>();
    private Transform rolePosition;

    //当前玩家角色类型
    private RoleType currentRoleType;
    private GameObject currentRoleGo;
    private PlayerAttack playerAttack;

    //同步位置的游戏体
    private RoleType syncRoleType;
    private GameObject syncRoleGo;

    //用于挂载同步request的游戏体
    private GameObject syncGo;

    //构造
    public PlayManger(GameFacade facade) : base(facade)
    {
    }

    public override void OnInit()
    {
        rolePosition = GameObject.Find("RolePosition").transform;
        IniRoleData();
    }

    //初始化角色数据
    private void IniRoleData()
    {
        roleDateDict.Add(RoleType.Blue,
            new RoleData(RoleType.Blue, "Prefabs/Hunter_BLUE", "Prefabs/Arrow_Blue",
                rolePosition.Find("position1").position));
        roleDateDict.Add(RoleType.Red,
            new RoleData(RoleType.Red, "Prefabs/Hunter_RED", "Prefabs/Arrow_Red",
                rolePosition.Find("position2").position));
    }

    //实例化角色
    public void SpawnRole()
    {
        foreach (RoleData rd in roleDateDict.Values)
        {
            GameObject go = Object.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
            go.tag = "Player";
            if (rd.RoleType == currentRoleType)
            {
                currentRoleGo = go;
            }
            else
            {
                syncRoleType = rd.RoleType;
                syncRoleGo = go;
            }
        }
    }

    //设置当前角色类型
    public void SetCurrentRoleType(RoleType roleType)
    {
        currentRoleType = roleType;
    }

    //获取当前角色游戏体
    public GameObject GetCurrentRoleGo()
    {
        return currentRoleGo;
    }

    //当前角色的游戏体添加控制脚本
    public void AddControlScript()
    {
        if (currentRoleGo.GetComponent<PlayerAttack>() == null)
        {
            playerAttack = currentRoleGo.AddComponent<PlayerAttack>();
            playerAttack.SetArrowData(roleDateDict[currentRoleType].ArrowPrefab);
        }

        if (currentRoleGo.GetComponent<PlayerMove>() == null)
        {
            currentRoleGo.AddComponent<PlayerMove>();
        }
    }

    //创建同步请求
    public void CreatSyncRequest()
    {
        //1、创建同步位置的请求
        syncGo = new GameObject("PlayerSyncRequest");
        MoveRequest moveRequest = syncGo.AddComponent<MoveRequest>();
        //设置本地角色和远程角色
        moveRequest.SetLocalPlayer(currentRoleGo.transform, currentRoleGo.GetComponent<PlayerMove>());
        moveRequest.SetRemotePlayer(syncRoleGo.transform, syncRoleGo.GetComponent<Animator>());
        //开始位置同步
        moveRequest.StartSync();

        //2、箭的位置同步
        ShootRequest shootRequest = syncGo.AddComponent<ShootRequest>();
        shootRequest.SetRemoteArrow(roleDateDict[syncRoleType].ArrowPrefab);
        playerAttack.SetShootRequest(shootRequest);

        //3、伤害同步
        AttackRequest attackRequest=syncGo.AddComponent<AttackRequest>();
        playerAttack.SetAttckRequest(attackRequest);
    }

    //游戏结束
    public void GameOver(RetuenCode retuenCode)
    {
        UserData.TotalCount++;

        if (retuenCode==RetuenCode.Sucess)
        {
            UserData.WinCount++;
        }

        Object.Destroy(currentRoleGo);
        Object.Destroy(syncRoleGo);
        Object.Destroy(syncGo);
    }

    public void QuitBattle()
    {
        Object.Destroy(currentRoleGo);
        Object.Destroy(syncRoleGo);
        Object.Destroy(syncGo);
    }
}