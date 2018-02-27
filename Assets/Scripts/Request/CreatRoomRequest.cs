using Common;

public class CreatRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Room;
        base._ActionCode = ActionCode.CreatRoom;
        roomPanel = GetComponent<RoomPanel>();
    }

    public void SetPanel(RoomPanel panel)
    {
        roomPanel = panel;
    }

    public void SendCreatRoomRequest()
    {
        base.SendRequest("r");
    }

    protected override void OnResponse(string data)
    {
        RetuenCode returnCode = (RetuenCode) int.Parse(data);
        roomPanel.OnCreatRoomResponse(returnCode);
    }
}