using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controlador/Camara")]
public class ValoresCamara : ScriptableObject
{
	public float suavizadoGiro = .1f;
	public float velMovimiento = 9;
    public float velApuntar = 0;
	public float velRotY = 8;
	public float velRotX = 8;
	public float anguloMin = -35;
	public float anguloMax = 35;
	public float normalZ;
	public float normalX;
	public float apuntarZ = -.5f;
	public float apuntarX = 0;
	public float normalY;
	public float agachadoY;
	public float adaptVel = 9;

}
