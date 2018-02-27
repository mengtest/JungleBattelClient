using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    private UIManager uiManager;//UI管类类
    private AudioManger audioManger;//声音管理类
    private PlayManger playManger;//玩家信息管理类
    private CameraManger cameraManger;//相机管理类
    private RequestManger requestManger;//向服务器发送请求的管理类
    private ClientManger clientManger;//和服务器连接的管理类

    private List<BaseManger> mangerList = new List<BaseManger>();

    private void Awake()
    {
        Instance = this;
        InitManger();
    }

    private void OnDestroy()
    {
        DestroyManger();
    }

    //构造所有的管理类,初始化所有的管理类
    private void InitManger()
    {
        uiManager = new UIManager(this);
        audioManger = new AudioManger(this);
        playManger = new PlayManger(this);
        cameraManger = new CameraManger(this);
        requestManger = new RequestManger(this);
        clientManger = new ClientManger(this);
        
        mangerList.Add(uiManager);
        mangerList.Add(audioManger);
        mangerList.Add(playManger);
        mangerList.Add(cameraManger);
        mangerList.Add(requestManger);
        mangerList.Add(clientManger);

        mangerList.ForEach(m => m.OnInit());
    }

    //提供游戏关闭时的生命周期函数
    private void DestroyManger()
    {
        mangerList.ForEach(m => m.OnDestory());
    }

    //添加请求实例到管理类
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestManger.AddRequest(actionCode, request);
    }

    //移除请求实例
    public void RemoveRequest(ActionCode actionCode)
    {
        requestManger.RemoveRequest(actionCode);
    }

    //向服务器发送请求
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientManger.SendRequest(requestCode,actionCode,data);
    }

    //处理接收到的服务器响应
    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestManger.HandleResponse(actionCode, data);
    }

    //播放背景音乐
    public void PlayBgSound(string soundName)
    {
        audioManger.PlayBgSounde(soundName);
    }

    //播放
    public void PlayNorSound(string soundName)
    {
        audioManger.PlayNorSound(soundName);
    }

    //取得玩家数据
    public UserData GetUserData()
    {
        return playManger.UserData;
    }

    //设置玩家数据
    public void SetUserData(UserData userData)
    {
        playManger.UserData = userData;
    }

    //显示提示UI
    public void ShowMsg(string msg)
    {
        uiManager.ShowMessage(msg);
    }

    //设置当前角色的类型
    public void SetCurrentRoleType(RoleType roleType)
    {
        playManger.SetCurrentRoleType(roleType);
    }

    //获取当前角色游戏体
    public GameObject GetCurrentGo()
    {
        return playManger.GetCurrentRoleGo();
    }

    //开始倒计时
    public void StartTimer()
    {
        playManger.SpawnRole();
        cameraManger.FollowRole();
    }

    //倒计时完毕，开始游戏
    public void StartPlaying()
    {
        playManger.AddControlScript();
        playManger.CreatSyncRequest();
    }

    //游戏结束
    public void GameOver(RetuenCode retuenCode)
    {
        playManger.GameOver(retuenCode);
        cameraManger.RoamScene();
    }

    //退出战斗
    public void QuitBattle()
    {
        playManger.QuitBattle();
        cameraManger.RoamScene();
    }
}
