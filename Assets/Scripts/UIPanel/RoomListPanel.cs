using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZJD;

//房间列表Panel
public class RoomListPanel : BasePanel
{
    private RectTransform battleInfo;
    private RectTransform roomList;
    private Text username;
    private Text totalCount;
    private Text winCount;
    private RectTransform listContent;
    private GameObject itemPrefab;

    private CreatRoomRequest creatRoomRequest;
    private ListRoomRequest listRoomRequest;
    private JoinRoomRequest joinRoomRequest;

    protected override void OnInit()
    {
        transform.GetInstance("BattleInfo", out battleInfo);
        transform.GetInstance("RoomList", out roomList);
        transform.GetInstance("BattleInfo/Username", out username);
        transform.GetInstance("BattleInfo/TotalCount", out totalCount);
        transform.GetInstance("BattleInfo/WinCount", out winCount);
        transform.GetInstance("RoomList/ScrollRect/Viewport/Content", out listContent);
        itemPrefab = Resources.Load<GameObject>("UIPanel/RoomItem");

        transform.GetInstance<Button>("RoomList/CloseButton").onClick.AddListener(OnCloseClick);
        transform.GetInstance<Button>("RoomList/CreatRoomButton").onClick.AddListener(OnCreatRoomClick);
        transform.GetInstance<Button>("RoomList/RefreshRoomButton").onClick.AddListener(OnRefreshClick);

        creatRoomRequest = GetComponent<CreatRoomRequest>();
        listRoomRequest = GetComponent<ListRoomRequest>();
        joinRoomRequest=GetComponent<JoinRoomRequest>();
    }

    public override void OnEnter()
    {
        EnterAni();
        SetBattleInfo();
        listRoomRequest.SendListRequest();
    }

    public override void OnExit()
    {
        HideAni();
    }

    public override void OnPause()
    {
        HideAni();
    }

    public override void OnResume()
    {
        EnterAni();
        SetBattleInfo();
        listRoomRequest.SendListRequest();
    }

    //关闭按钮回调
    private void OnCloseClick()
    {
        PlayClickSound();
        UIMng.PopPanel();
    }

    //创建按钮回调
    private void OnCreatRoomClick()
    {
        RoomPanel panel = UIMng.PushPanel(UIPanelType.Room) as RoomPanel;
        creatRoomRequest.SetPanel(panel);
        creatRoomRequest.SendCreatRoomRequest();
    }

    //刷新按钮回调
    private void OnRefreshClick()
    {
        listRoomRequest.SendListRequest();
    }

    private void EnterAni()
    {
        gameObject.SetActive(true);

        battleInfo.localPosition = new Vector3(-1000, 0);
        battleInfo.DOLocalMoveX(-302, 0.5f);

        roomList.localPosition = new Vector3(1000, 0);
        roomList.DOLocalMoveX(114, 0.5f);
    }

    private void HideAni()
    {
        battleInfo.DOLocalMoveX(-1000, 0.5f);
        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    //设置自身信息
    private void SetBattleInfo()
    {
        UserData userData = Facade.GetUserData();
        username.text = userData.Username;
        totalCount.text = "总场数：" + userData.TotalCount;
        winCount.text = "胜利场数：" + userData.WinCount;
    }

    //加载单个房间的Item
    public void LoadRoomItem(List<UserData> udList)
    {
        foreach (RoomItem item in listContent.GetComponentsInChildren<RoomItem>())
        {
            item.DestorySelf();
        }

        if (udList==null)  return;

        foreach (UserData ud in udList)
        {
            GameObject item = Instantiate(itemPrefab);
            item.transform.SetParent(listContent);
            item.GetComponent<RoomItem>().SetRoomInfo(this, ud.ID, ud.Username, ud.TotalCount, ud.WinCount);
        }
    }

    //单个房间加入按钮点击时调用
    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendJoinRoomRequest(id);
    }

    //加入房间时的响应
    public void OnJoinResponse(RetuenCode returnCode, UserData owner, UserData self)
    {
        switch (returnCode)
        {
            case RetuenCode.Sucess:
                RoomPanel roomPanel = UIMng.PushPanel(UIPanelType.Room) as RoomPanel;
                if (roomPanel != null)
                {
                    roomPanel.SetBlueResult(owner.Username, owner.TotalCount.ToString(), owner.WinCount.ToString());
                    roomPanel.SetRedResult(self.Username,self.TotalCount.ToString(),self.WinCount.ToString());
                    Facade.SetCurrentRoleType(RoleType.Red);//设置当前角色类型为红色
                }

                break;
            case RetuenCode.Fail:
                Facade.ShowMsg("房间已满，无法加入");
                break;
            case RetuenCode.NotFound:
                Facade.ShowMsg("房间未找到");
                break;
        }
    }
}