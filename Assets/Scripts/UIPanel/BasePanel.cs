using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected GameFacade Facade { get; private set; }
    protected UIManager UIMng { get; private set; }

    public void Init(GameFacade facade,UIManager uiManager)
    {
        Facade = facade;
        UIMng = uiManager;
        OnInit();
    }

    /// 初始化
    protected virtual void OnInit()
    {
    }

    /// 界面被显示出来
    public virtual void OnEnter()
    {

    }

    /// 界面暂停
    public virtual void OnPause()
    {

    }

    /// 界面继续
    public virtual void OnResume()
    {

    }

    /// 界面不显示,退出这个界面，界面被关闭
    public virtual void OnExit()
    {

    }

    /// 播放点击声音
    protected void PlayClickSound()
    {
        Facade.PlayNorSound(AudioManger.Sound_ButtonClick);
    }
}
