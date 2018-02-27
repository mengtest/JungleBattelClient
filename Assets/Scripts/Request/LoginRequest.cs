using Common;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.User;
        base._ActionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
    }

    public void SendLoginRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    protected override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        RetuenCode returnCode = (RetuenCode) int.Parse(strs[0]);

        if (returnCode == RetuenCode.Sucess)
        {
            string username = strs[1];
            int totalCount = int.Parse(strs[2]);
            int winCount = int.Parse(strs[3]);

            UserData userData = new UserData(username, totalCount, winCount);
            Facade.SetUserData(userData);
        }

        loginPanel.OnLoginRespose(returnCode);
    }
}