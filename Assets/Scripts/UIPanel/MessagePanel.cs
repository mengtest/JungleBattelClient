using DG.Tweening;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private const float Timer = 1f;

    protected override void OnInit()
    {
        text = transform.Find("Message").GetComponent<Text>();
    }

    public override void OnEnter()
    {
        text.DOFade(0, 0);
    }

    //显示提示内容
    public void ShowMessage(string msg)
    {
        text.text = msg;
        text.DOFade(1, Timer / 2).OnComplete(() => text.DOFade(0, Timer / 2));
    }
}