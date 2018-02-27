using UnityEngine;
using UnityEngine.UI;

//单个房间Item
public class RoomItem : MonoBehaviour
{
    public int Id { get; private set; }
    public Text Username;
    public Text TotalCount;
    public Text WinCount;
    public Button JoinButton;

    private RoomListPanel roomListPanel;//房间列表的panel

    private void Start()
    {
        if (JoinButton != null)
        {
            JoinButton.onClick.AddListener(JoinBtnCallback);
        }
    }
    
    //加入房间按钮回调
    private void JoinBtnCallback()
    {
        roomListPanel.OnJoinClick(Id);
    }

    //设置显示信息
    public void SetRoomInfo(RoomListPanel panel, int id, string username, int totalCount, int winCount)
    {
        roomListPanel = panel;
        Id = id;
        Username.text = username;
        TotalCount.text = "总场数\n" + totalCount;
        WinCount.text = "胜利场数\n" + winCount;
    }

    //设置显示信息
    public void SetRoomInfo(RoomListPanel panel, string id, string username, string totalCount, string winCount)
    {
        SetRoomInfo(panel,int.Parse(id),username,int.Parse(totalCount),int.Parse(winCount));
    }

    //删除自身
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}