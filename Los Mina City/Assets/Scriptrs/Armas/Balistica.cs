using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balistica : MonoBehaviour
{
    public static RaycastHit RaycastDisparo(Vector3 o, Vector3 d, ref bool s, LayerMask lm)
    {
        Ray ray = new Ray(o,d);
        RaycastHit hit;
        if (Physics.Raycast(o, d, out hit, 100, lm))
        {
            s = true;
        }

        return hit;
    }
}
