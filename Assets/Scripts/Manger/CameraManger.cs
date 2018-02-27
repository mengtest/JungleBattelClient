using DG.Tweening;
using UnityEngine;

public class CameraManger : BaseManger
{
    private GameObject cameraGo;
    private Animator cameraAni;
    private FollowTarget followTarget;

    private Vector3 originalPos;
    private Vector3 originalRot;

    public CameraManger(GameFacade facade) : base(facade)
    {
    }

    public override void OnInit()
    {
        cameraGo = Camera.main.gameObject;
        cameraAni=cameraGo.GetComponent<Animator>();
        followTarget=cameraGo.GetComponent<FollowTarget>();

    }

    //相机跟随目标（当前角色）
    public void FollowRole()
    {
        //保存当前的位置和旋转
        originalPos = cameraGo.transform.position;
        originalRot = cameraGo.transform.eulerAngles;

        //禁用漫游动画、启用跟随
        cameraAni.enabled = false;
        Transform target = Facade.GetCurrentGo().transform;
        cameraGo.transform.DOLookAt(target.position-cameraGo.transform.position,1).OnComplete(() =>
        {
            followTarget.target = target;
            followTarget.enabled = true;
        });
    }

    //相机场景漫游
    public void RoamScene()
    {
        followTarget.enabled = false;

        cameraGo.transform.DOMove(originalPos, 1);
        cameraGo.transform.DORotate(originalRot, 1f).OnComplete(() =>
        {
            cameraAni.enabled = true;
        });
    }

}
