using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/GameObject")]
public class ObjectGame : ScriptableObject
{
    public Object valor;

    public void Objeto(GameObject go)
    {
        valor = go;
    }
}
