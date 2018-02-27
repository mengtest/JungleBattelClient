using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;
    private Vector3 offset=new Vector3(0,12,-22);
    private float smoothing = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 targetPos = target.position + offset;
        transform.position= Vector3.Lerp(transform.position,targetPos,smoothing*Time.deltaTime);
        transform.LookAt(target);
	}
}
