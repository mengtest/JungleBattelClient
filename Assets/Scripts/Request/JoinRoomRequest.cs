using Common;

public class JoinRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;
//    private RoomPanel roomPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Room;
        base._ActionCode = ActionCode.JoinRoom;
        roomListPanel = GetComponent<RoomListPanel>();
    }

    public void SendJoinRoomRequest(int id)
    {
        base.SendRequest(id.ToString());
    }

    protected override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        RetuenCode returnCode = (RetuenCode)int.Parse(strs[0]);
        UserData owner = null;
        UserData self = null;

        if (returnCode == RetuenCode.Sucess)
        {
            owner = new UserData(-1, strs[1], int.Parse(strs[2]), int.Parse(strs[3]));
            self = Facade.GetUserData();
        }
        
        roomListPanel.OnJoinResponse(returnCode, owner, self);
    }
}
