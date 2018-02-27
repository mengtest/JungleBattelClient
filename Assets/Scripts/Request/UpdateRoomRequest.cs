using System;
using Common;

public class UpdateRoomRequest:BaseRequest
{
    private RoomPanel roomPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Room;
        base._ActionCode = ActionCode.UpdateRoom;
        roomPanel = GetComponent<RoomPanel>();
    }

    //加入房间请求的响应
    protected override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        RetuenCode returnCode = (RetuenCode)int.Parse(strs[0]);

        switch (returnCode)
        {
            case RetuenCode.Join:
                roomPanel.OnUpdateRoomResponse(returnCode, strs[1], strs[2], strs[3]);
                break;
            case RetuenCode.Quit:
                roomPanel.OnUpdateRoomResponse(returnCode);
                break;
        }
    }
}

