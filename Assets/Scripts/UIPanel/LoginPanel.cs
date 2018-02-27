using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button closeButton;
    private InputField nameInput;
    private InputField pwdInput;
    private Button loginButton;
    private Button registerButton;

    private LoginRequest loginRequest;

    protected override void OnInit()
    {
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);

        nameInput = transform.Find("UserName/InputField").GetComponent<InputField>();
        pwdInput = transform.Find("PassWord/InputField").GetComponent<InputField>();

        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);

        registerButton = transform.Find("RegisterButton").GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);

        loginRequest = gameObject.GetComponent<LoginRequest>();
    }

    public override void OnEnter()
    {
        EnterAni();
    }

    public override void OnExit()
    {
        HideAni();
    }

    public override void OnPause()
    {
        HideAni();
        
    }

    public override void OnResume()
    {
        EnterAni();
    }

    private void EnterAni()
    {
        gameObject.SetActive(true);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);

        transform.localPosition = new Vector3(-1000, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    private void HideAni()
    {
        transform.DOScale(0, 0.5f);
        transform.DOLocalMove(new Vector3(-1000, 0), 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        UIMng.PopPanel();
    }

    //登录回调
    private void OnLoginClick()
    {
        PlayClickSound();
        if (string.IsNullOrEmpty(nameInput.text) || string.IsNullOrEmpty(pwdInput.text))
        {
            UIMng.ShowMessage("用户名和密码不能为空");
        }
        else
        {
            loginRequest.SendLoginRequest(nameInput.text,pwdInput.text);
        }
    }

    //登录响应
    public void OnLoginRespose(RetuenCode retuenCode)
    {
        switch (retuenCode)
        {
            case RetuenCode.Sucess:
                UIMng.PushPanel(UIPanelType.RoomList);
                break;
            case RetuenCode.Fail:
                UIMng.ShowMessage("用户名密码错误");
                break;
        }
    }

    //注册回调
    private void OnRegisterClick()
    {
        PlayClickSound();
        UIMng.PushPanel(UIPanelType.Register);
    }
}