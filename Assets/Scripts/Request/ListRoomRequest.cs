using System.Collections.Generic;
using System.Linq;
using Common;

public class ListRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Room;
        base._ActionCode = ActionCode.ListRoom;
        roomListPanel=GetComponent<RoomListPanel>();
    }

    protected override void OnResponse(string data)
    {
        if (data=="0")
        {
            roomListPanel.LoadRoomItem(null);
            return;
        }

        string[] roomOwnerArray = data.Split('|');
        List<UserData> useDataList =roomOwnerArray.//"(1,siki,0,0),(2,zjd,0,0)"
            Select(userDataStr => userDataStr.Split(',')).
            Select(userDataArray => new UserData(int.Parse(userDataArray[0]), userDataArray[1], int.Parse(userDataArray[2]),int.Parse(userDataArray[3]))).
            ToList();

        roomListPanel.LoadRoomItem(useDataList);
    }

    
    public void SendListRequest()
    {
        base.SendRequest("refresh");
    }
}
