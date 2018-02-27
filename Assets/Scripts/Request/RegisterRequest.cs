using Common;

public class RegisterRequest : BaseRequest {

    private RegisterPanel registerPanel;
    private void Awake()
    {
        base._RequestCode = RequestCode.User;
        base._ActionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
    }

    public void SendRegisterRequest(string username, string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }

    protected override void OnResponse(string data)
    {
        RetuenCode returnCode = (RetuenCode)int.Parse(data);
        registerPanel.OnRegisterResponse(returnCode);
    }
}
