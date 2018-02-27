using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    private Rigidbody rig;
    private AttackRequest attackRequest;
    private int Speed = 10;

    private void Awake()
    {
        rig=GetComponent<Rigidbody>();
    }

    public void SetArrowData(AttackRequest request)
    {
        attackRequest = request;
    }

	void Update ()
    {
        rig.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GameFacade.Instance.PlayNorSound(AudioManger.Sound_ShootPerson);
            if (attackRequest!=null)
            {
                attackRequest.SendAttackRequest(Random.Range(10,20));
            }
        }
        else
        {
            GameFacade.Instance.PlayNorSound(AudioManger.Sound_Miss);
        }

        Instantiate(explosionEffect,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
