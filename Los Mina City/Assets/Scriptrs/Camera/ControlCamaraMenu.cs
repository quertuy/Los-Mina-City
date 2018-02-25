using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ControlCamaraMenu : MonoBehaviour
{
    public Transform mTransform;

    private void FixedUpdate()
    {
        Quaternion rot = Quaternion.LookRotation(mTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10);
    }

    public void ObjetoAMirar(Transform t)
    {
        mTransform = t;
    }
}
