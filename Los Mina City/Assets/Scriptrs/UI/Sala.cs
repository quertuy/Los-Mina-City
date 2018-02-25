using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sala")]
public class Sala : ScriptableObject
{
    public string nivelObjetivo;
    public TipoSala tipo;
    public int maxJugadores = 15;


}
public enum TipoSala
{
    tiroteo, equipos
}
