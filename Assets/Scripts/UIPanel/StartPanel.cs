using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button loginButton;

    protected override void OnInit()
    {
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    public override void OnPause()
    {
        loginButton.gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        loginButton.gameObject.SetActive(true);
    }

    //登录按钮点击回调
    private void OnLoginClick()
    {
        PlayClickSound();
        UIMng.PushPanel(UIPanelType.Login);
    }
}
