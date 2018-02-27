using Common;

public class QuitRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Room;
        base._ActionCode = ActionCode.QuitRoom;
        roomPanel=GetComponent<RoomPanel>();
    }

    public void SendQuitRoomRequest()
    {
        base.SendRequest("quit");
    }

    protected override void OnResponse(string data)
    {
        string[] strs=data.Split(',');

        RetuenCode returnCode = (RetuenCode)int.Parse(strs[0]);
        if (returnCode==RetuenCode.Sucess)
        {
            roomPanel.OnQuitResponse();
        }
    }
}
