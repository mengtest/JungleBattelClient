using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZJD;

public class Test : MonoBehaviour
{
    public Transform target;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.BackAt(target.position);
	}
}
