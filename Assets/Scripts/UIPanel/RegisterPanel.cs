using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZJD;

public class RegisterPanel : BasePanel
{
    private Button closeButton;
    private InputField userInput;
    private InputField pwdInput;
    private InputField rePwdInput;
    private Button registerButton;

    private RegisterRequest registerRequest;

    protected override void OnInit()
    {
        closeButton = transform.GetInstance<Button>("CloseButton");
        closeButton.onClick.AddListener(OnCloseClick);

        userInput = transform.GetInstance<InputField>("UserName/InputField");
        pwdInput = transform.GetInstance<InputField>("PassWord/InputField");
        rePwdInput = transform.GetInstance<InputField>("RePassWord/InputField");

        registerButton = transform.GetInstance<Button>("RegisterButton");
        registerButton.onClick.AddListener(OnRegisterClick);

        registerRequest=GetComponent<RegisterRequest>();

        #region MyRegion
        //        transform.GetInstance("CloseButton",out closeButton);
        //        transform.GetInstance("UserName/InputField",out userInput);
        //        transform.GetInstance("PassWord/InputField", out pwdInput);
        //        transform.GetInstance("RePassWord/InputField", out rePwdInput);
        //        transform.GetInstance("RegisterButton", out registerButton);
        #endregion
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);

        transform.localPosition = new Vector3(1000, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    public override void OnExit()
    {
        transform.DOScale(0, 0.5f);
        transform.DOLocalMove(new Vector3(1000, 0), 0.5f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        UIMng.PopPanel();
    }

    //注册回调
    private void OnRegisterClick()
    {
        PlayClickSound();
        if (string.IsNullOrEmpty(userInput.text) || string.IsNullOrEmpty(pwdInput.text) || string.IsNullOrEmpty(rePwdInput.text))
        {
            UIMng.ShowMessage("用户名和密码不能为空");
            return;
        }

        if (pwdInput.text != rePwdInput.text)
        {
            UIMng.ShowMessage("两次输入的密码不一致");
        }

        registerRequest.SendRegisterRequest(userInput.text,pwdInput.text);
    }

    //注册响应
    public void OnRegisterResponse(RetuenCode retuenCode)
    {
        switch (retuenCode)
        {
            case RetuenCode.Sucess:
                UIMng.ShowMessage("注册成功");
                break;
            case RetuenCode.Fail:
                UIMng.ShowMessage("用户名重复");
                break;
        }

    }
}
