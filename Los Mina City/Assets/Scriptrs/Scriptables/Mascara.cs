using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Personaje/Mascara")]
public class Mascara : ScriptableObject
{
    public string id;
    public bool pelo;
    public bool ojos;
    public ObjetoPersonaje obj;

}
