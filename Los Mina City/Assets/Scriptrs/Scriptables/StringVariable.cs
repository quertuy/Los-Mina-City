using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String")]
public class StringVariable : ScriptableObject
{
    public string valor;

    public void Set(string val)
    {
        valor = val;
    }
}
