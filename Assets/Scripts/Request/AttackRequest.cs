using Common;

public class AttackRequest : BaseRequest 
{
    private void Awake()
    {
        base._RequestCode = RequestCode.Game;
        base._ActionCode = ActionCode.Attack;
    }

    public void SendAttackRequest(int damange)
    {
        base.SendRequest(damange.ToString());
    }
}
