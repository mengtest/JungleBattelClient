﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryForTime : MonoBehaviour
{
    [SerializeField]private float time = 1;

    private void Start()
    {
        Destroy(gameObject,time);
    }
}
