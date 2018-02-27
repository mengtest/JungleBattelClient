using Common;

public class StartGameRequest : BaseRequest
{
    private RoomPanel roomPanel;
    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.StartGame;
        roomPanel = GetComponent<RoomPanel>();
    }

    public void SendStartRequest()
    {
        base.SendRequest("start");
    }

    protected override void OnResponse(string data)
    {
        RetuenCode requestCode = (RetuenCode)int.Parse(data);
        roomPanel.OnStartResponse(requestCode);
    }

}