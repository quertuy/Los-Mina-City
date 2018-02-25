using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Personajes/ContenedorMalla")]
public class ContenedorMalla : ScriptableObject
{
    public string id;
    public Mesh mallaHombre;
    public Mesh mallaMujer;
    public Material material;
}
