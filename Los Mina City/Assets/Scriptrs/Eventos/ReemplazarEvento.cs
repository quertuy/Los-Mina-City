using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReemplazarEvento : MonoBehaviour
{
    public EventosJuego eventoObjetivo;
    public EventosJuegoListener listener;

    public void Reemplazar()
    {
        listener.eventosJuego.NoResgistrar(listener);
        listener.eventosJuego = (eventoObjetivo);
        eventoObjetivo.Registrar(listener);
    }
}
