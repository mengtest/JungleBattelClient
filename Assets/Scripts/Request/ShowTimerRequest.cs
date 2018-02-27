using Common;

public class ShowTimerRequest : BaseRequest
{
    private GamePanel gamePanel;
    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.ShowTimer;
        gamePanel = GetComponent<GamePanel>();
    }

    protected override void OnResponse(string data)
    {
        gamePanel.ShowTime(int.Parse(data));
    }
}
