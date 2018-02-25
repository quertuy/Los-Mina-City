using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class Float : ScriptableObject
{
    public float valor;

    public void Aplicar(Float v)
    {
        valor += v.valor;
    }

    public void Aplicar(float v)
    {
        valor += v;
    }
}


