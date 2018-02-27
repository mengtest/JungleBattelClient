using Common;

public class StartPlayRequest:BaseRequest
{
    private GamePanel gamePanel;
    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.StartPlay;
        gamePanel = GetComponent<GamePanel>();
    }

    protected override void OnResponse(string data)
    {
        gamePanel.ShowExitBtn();
        Facade.StartPlaying();
    }
}
