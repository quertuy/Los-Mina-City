using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Armas/Arma", order = 0)]
public class Arma : ScriptableObject
{
    public string nombre;

    public PosicionIK posicionIK;
    public GameObject modelo;

    public SonidoArma sonidoArma;

    public float radioDisparo;
    public int municion = 30;
    public int municionMaxima = 90;
    public int tipoArma;

    public AnimationCurve retrocesoY;
    public AnimationCurve retrocesoZ;
}
[System.Serializable]
public class SonidoArma
{
    public GestorAudio disparando;
    public GestorAudio recargando;
}
public enum TipoArma
{
    principal, segundaria, cuerpoACuerpo
}
