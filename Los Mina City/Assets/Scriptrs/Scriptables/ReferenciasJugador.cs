using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/ReferenciasJugador")]
public class ReferenciasJugador : ScriptableObject
{
    public Int cargador;
    public Int municion;
    public Int vida;

    public Bool apuntando;
    public Bool pivotIzq;
    public Bool agachado;

    public Float extencionObjetivo;
    public EventosJuego updateUI;

    public void Init()
    {
        municion.valor = 0;
        cargador.valor = 0;
        vida.valor = 100;
        apuntando.valor = false;
        pivotIzq.valor = false;
        agachado.valor = false;
        extencionObjetivo.valor = 25;
    }
}
