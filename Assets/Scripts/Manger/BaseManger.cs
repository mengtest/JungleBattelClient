public class BaseManger
{
    protected readonly GameFacade Facade;
    protected BaseManger(GameFacade facade){ Facade = facade; }
    public virtual void OnInit(){ }
//    public virtual void Update(){ }
    public virtual void OnDestory(){ }
}