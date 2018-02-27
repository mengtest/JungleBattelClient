using UnityEngine;
using ZJD;

public class PlayerMove : MonoBehaviour
{
    private const float Speed = 3;
    private Animator ani;

    public float Forward = 0;


    void Awake()
    {
        ani = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName(MyAnimator.State.Grounded) == false) return;

        float h = Input.GetAxis(MyAxis.Horizontal);
        float v = Input.GetAxis(MyAxis.Vertical);

        if (h == 0 && v == 0)
        {
            return;
        }

        transform.Translate(new Vector3(h, 0, v) * Speed * Time.deltaTime, Space.World); //移动控制
        transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v)); //旋转控制

        float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v)); //动画控制
        Forward = res;
        ani.SetFloat(MyAnimator.Float.Forward, res);
    }
}