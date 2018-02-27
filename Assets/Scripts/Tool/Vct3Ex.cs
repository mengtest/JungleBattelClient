using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vct3Ex 
{
    public static Vector3 Parse(string pos)
    {
        string[] p = pos.Split(',');
        return new Vector3(float.Parse(p[0]),float.Parse(p[1]),float.Parse(p[2]));
    }

}
