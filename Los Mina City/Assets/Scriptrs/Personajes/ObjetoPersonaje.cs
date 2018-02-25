using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Personaje/Objeto")]
public class ObjetoPersonaje : ScriptableObject
{
    public string id;
    
    public GameObject h_prefab;
    public GameObject m_prefab;
}

public enum MisHuesos
{
    cabeza, cintura, manoIzq, manoDer
}
