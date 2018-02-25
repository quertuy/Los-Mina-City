using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controlador/Estadisticas")]
public class ContorladoEstadisticas : ScriptableObject 
{
	public float velMovimiento = 4;
	public float velCorrer = 6;
	public float velAgachado = 2;
	public float velAnimacion = 2;
	public float velRotacion = 8;

    public float umbralMovimiento = .5f;
    public float umbralAngulo = 30;
    public float distanciaOla = 1;
    public float anguloOla = 30;
    public float prediccionVel = 9;
}
