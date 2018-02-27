using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameOverRequest : BaseRequest
{
    private GamePanel gamePanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.GameOver;
        gamePanel=GetComponent<GamePanel>();
    }

    protected override void OnResponse(string data)
    {
        RetuenCode retuenCode = (RetuenCode) int.Parse(data);
        gamePanel.OnGameOverResponse(retuenCode);
    }
}