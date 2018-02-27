using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class QuitBattleRequest : BaseRequest
{
    private GamePanel gamePanel;

    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.QuitBattle;
        gamePanel=GetComponent<GamePanel>();
    }

    public void SendQuitBattleRequest()
    {
        base.SendRequest("QuitBattle");
    }

    protected override void OnResponse(string data)
    {
        gamePanel.OnQuitBattleResponse();
    }
}
