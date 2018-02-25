using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeplazarEventoSesion : MonoBehaviour
{
    public GestorSesion ses;
    public EventosJuego eventoObjetivo;

    public void ReemplazarUnEvento()
    {
        ses.todosMisEventos.cargarUnaEscena = eventoObjetivo;
    }

    public void RemmplazarEscenaAditiva()
    {
        ses.todosMisEventos.escenaAditiva = eventoObjetivo;
    }

	
}
