using UnityEngine;
using ZJD;

public class PlayerAttack : MonoBehaviour
{
    private Animator ani;
    private GameObject arrowPrefab; //需要实例的预制体
    private Transform arrowBone; //实例时箭的初始位置
    private ShootRequest shootRequest; //射击请求实例
    private AttackRequest attackRequest;//伤害请求实例

    private Vector3 target; //临时变量，用于记录箭的目标点

    void Awake()
    {
        ani = GetComponent<Animator>();
        string path =
            "Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/Bip001 R Hand Prop";
        arrowBone = transform.Find(path);
    }

    void Update()
    {
        ShootCheck();
    }

    //设置射击请求实例
    public void SetShootRequest(ShootRequest shootRequest)
    {
        this.shootRequest = shootRequest;
    }    
    
    //设置伤害请求实例
    public void SetAttckRequest(AttackRequest attackRequest)
    {
        this.attackRequest = attackRequest;
    }

    //设置箭的预制体
    public void SetArrowData(GameObject arrowPre)
    {
        arrowPrefab = arrowPre;
    }

    //射击检测
    private void ShootCheck()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(MyAnimator.State.Grounded) == false) return; //判断当前动画状态
        if (Input.GetMouseButtonDown(0) == false) return; //判断是否点击了鼠标左键

        //发射射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit);
        if (isHit == false) return; //判断射线是否碰撞到碰撞体

        //朝向攻击目标点
        target = hit.point;
        target.y = transform.position.y;
        transform.LookAt(target);

        //播放攻击动画
        ani.SetTrigger(MyAnimator.Trigger.Attack);
        //实例化箭
        Invoke("Shoot", 0.65f);
    }

    //射击
    private void Shoot()
    {
        //实例化箭
        target.y = arrowBone.position.y;
        Vector3 dir = target - arrowBone.position;
        GameObject go= Instantiate(arrowPrefab, arrowBone.position, Quaternion.LookRotation(dir));

        //标设置箭的数据
        Arrow arrow = go.GetComponent<Arrow>();
        arrow.SetArrowData(attackRequest);

        //向服务器发起同步箭的请求
        shootRequest.SendShootRequest(arrowBone.position, Quaternion.LookRotation(dir).eulerAngles);

        //播放发射箭的声音
        GameFacade.Instance.PlayNorSound(AudioManger.Sound_Timer);
    }
}