using Common;
using UnityEngine.UI;
using ZJD;

public class RoomPanel : BasePanel
{
    private Text blueUsername;
    private Text blueTotalCount;
    private Text blueWinCount;

    private Text redUsername;
    private Text redTotalCount;
    private Text redWinCount;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;

    protected override void OnInit()
    {
        base.OnInit();

        transform.GetInstance("Blue/username", out blueUsername);
        transform.GetInstance("Blue/totalcount", out blueTotalCount);
        transform.GetInstance("Blue/wincount", out blueWinCount);

        transform.GetInstance("Red/username", out redUsername);
        transform.GetInstance("Red/totalcount", out redTotalCount);
        transform.GetInstance("Red/wincount", out redWinCount);

        transform.GetInstance<Button>("StartButton").onClick.AddListener(StartGameCallback);
        transform.GetInstance<Button>("CloseButton").onClick.AddListener(QuitClickCallback);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
    }

    public override void OnEnter()
    {
        EnterAni();
    }

    public override void OnPause()
    {
        ExitAni();
    }

    public override void OnResume()
    {
        EnterAni();
    }

    public override void OnExit()
    {
        ExitAni();
    }

    //开始游戏的回调
    private void StartGameCallback()
    {
        startGameRequest.SendStartRequest();
    }

    //开始游戏的响应
    public void OnStartResponse(RetuenCode retuenCode)
    {
        if (retuenCode==RetuenCode.Sucess)
        {
            UIMng.PushPanel(UIPanelType.Game);
            Facade.StartTimer();//开始倒计时
        }
        else if (retuenCode==RetuenCode.Fail)
        {
            UIMng.ShowMessage("您不是房主，无法开始游戏！！！");
        }
    }

    //退出房间回调
    private void QuitClickCallback()
    {
        quitRoomRequest.SendQuitRoomRequest();
    }

    //退出房间响应
    public void OnQuitResponse()
    {
        UIMng.PopPanel();
    }

    //创建房间的响应
    public void OnCreatRoomResponse(RetuenCode retuenCode)
    {
        switch (retuenCode)
        {
            case RetuenCode.Sucess:
                UserData userData = Facade.GetUserData();
                SetBlueResult(userData.Username, userData.TotalCount.ToString(), userData.WinCount.ToString());
                ClearRedResult();
                Facade.SetCurrentRoleType(RoleType.Blue);//设置当前角色类型为蓝色
                break;
            case RetuenCode.Fail:
                break;
        }
    }

    //更新房间的响应
    public void OnUpdateRoomResponse(RetuenCode retuenCode, string username = null, string totalCount = null,
        string winCount = null)
    {
        switch (retuenCode)
        {
            case RetuenCode.Join: //其他客户端加入
                SetRedResult(username, totalCount, winCount);
                break;
            case RetuenCode.Quit: //其他客户端离开
                ClearRedResult();
                break;
        }
    }

    //设置蓝色面板战绩
    public void SetBlueResult(string username, string totalCount, string winCount)
    {
        blueUsername.text = username;
        blueTotalCount.text = "总场数：" + totalCount;
        blueWinCount.text = "胜利场数：" + winCount;
    }

    //设置红色面板战绩
    public void SetRedResult(string username, string totalCount, string winCount)
    {
        redUsername.text = username;
        redTotalCount.text = "总场数：" + totalCount;
        redWinCount.text = "胜利场数：" + winCount;
    }

    //清除红色面板信息
    private void ClearRedResult()
    {
        redUsername.text = "";
        redTotalCount.text = "等待玩家加入";
        redWinCount.text = "";
    }

    private void EnterAni()
    {
        //todo,添加UI动画
        gameObject.SetActive(true);
    }

    private void ExitAni()
    {
        //todo,添加UI动画
        gameObject.SetActive(false);
    }
}