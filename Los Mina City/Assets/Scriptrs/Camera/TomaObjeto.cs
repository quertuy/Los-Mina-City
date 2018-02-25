using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomaObjeto : MonoBehaviour
{
    public ObjectGame objeto;

    private void Start()
    {
        objeto.Objeto(this.gameObject);
    }
}
