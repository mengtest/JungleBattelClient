using UnityEngine;
using System.Collections.Generic;
using System;
using ZJD;
using Object = UnityEngine.Object;

public class UIManager:BaseManger
{
    private Transform canvasTransform;

    private Dictionary<UIPanelType, BasePanel> panelDict=new Dictionary<UIPanelType, BasePanel>();//保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack=new Stack<BasePanel>();

    public UIManager(GameFacade facade) : base(facade)
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    public override void OnInit()
    {
        PushPanel(UIPanelType.Message);
        PushPanel(UIPanelType.Start);
    }

    // 把某个页面入栈，把页面显示在界面上
    public BasePanel PushPanel(UIPanelType panelType)
    {
        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);

        return panel;
    }

    // 出栈 ，把页面从界面上移除
    public void PopPanel()
    {
        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    // 根据面板类型 得到实例化的面板
    private BasePanel GetPanel(UIPanelType panelType)
    {
        BasePanel panel = panelDict.TryGet(panelType);

        if (panel != null) return panel;

        //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
        string path = string.Format("UIPanel/{0}Panel",panelType);
        GameObject instPanel = Object.Instantiate(Resources.Load(path)) as GameObject;

        if (instPanel == null)
        {
            Debug.Log(string.Format("未能实例化指定[{0}]类型的Panel", panelType));
            return null;
        }

        instPanel.transform.SetParent(canvasTransform, false);
        BasePanel basePanel = instPanel.GetComponent<BasePanel>();
        basePanel.Init(Facade,this);
        panelDict.Add(panelType, basePanel);
        return basePanel;
    }

    //显示提示文字
    public void ShowMessage(string msg)
    {
        MessagePanel msgPanel = GetPanel(UIPanelType.Message) as MessagePanel;
        if (msgPanel != null) msgPanel.ShowMessage(msg);
    }
}
