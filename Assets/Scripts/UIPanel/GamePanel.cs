using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZJD;

public class GamePanel : BasePanel
{
    private Text timer;
    private Button successBtn;
    private Button failBtn;
    private Button exitBtn;

    private QuitBattleRequest quitBattleRequest;

    protected override void OnInit()
    {
        transform.GetInstance("Timer",out timer);
        timer.gameObject.SetActive(false);

        transform.GetInstance("Success",out successBtn);
        successBtn.gameObject.SetActive(false);
        successBtn.onClick.AddListener(ResultCallback);

        transform.GetInstance("Fail", out failBtn);
        failBtn.gameObject.SetActive(false);
        failBtn.onClick.AddListener(ResultCallback);

        transform.GetInstance("Exit", out exitBtn);
        exitBtn.gameObject.SetActive(false);
        exitBtn.onClick.AddListener(ExitCallback);

        quitBattleRequest=GetComponent<QuitBattleRequest>();
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        successBtn.gameObject.SetActive(false);
        failBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    //显示倒计时
    public void ShowTime(int time)
    {
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();

        timer.transform.DOScale(1, 0);
        timer.DOFade(1, 0);
        timer.transform.DOScale(2,0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));

        Facade.PlayNorSound(AudioManger.Sound_Alert);
    }

    //显示退出按钮
    public void ShowExitBtn()
    {
        exitBtn.gameObject.SetActive(true);
    }

    //处理退出游戏时的响应
    public void OnQuitBattleResponse()
    {
        Facade.QuitBattle();
        UIMng.PopPanel();
        UIMng.PopPanel();
    }

    //处理游戏结束时的响应
    public void OnGameOverResponse(RetuenCode retuenCode)
    {
        //1、显示战斗结果文字
        Button tempBtn = null;

        if (retuenCode==RetuenCode.Sucess)
        {
            tempBtn = successBtn;
        }

        if (retuenCode==RetuenCode.Fail)
        {
            tempBtn = failBtn;
        }

        if (tempBtn == null) return;
        tempBtn.gameObject.SetActive(true);
        tempBtn.transform.localScale = Vector3.zero;
        tempBtn.transform.DOScale(1, 0.4f);

        //2、出去其他模块
        Facade.GameOver(retuenCode);
    }

    //游戏结束时文字点击回调
    private void ResultCallback()
    {
        UIMng.PopPanel();
        UIMng.PopPanel();
    }

    //退出按钮回调
    private void ExitCallback()
    {
        quitBattleRequest.SendQuitBattleRequest();
    }
}
