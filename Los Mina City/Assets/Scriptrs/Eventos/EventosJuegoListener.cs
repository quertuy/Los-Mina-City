using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventosJuegoListener : MonoBehaviour
{
    public EventosJuego eventosJuego;
    public UnityEvent respuesta;

    private void OnEnable()
    {
        eventosJuego.Registrar(this);
    }

    private void OnDisable()
    {
        eventosJuego.NoResgistrar(this);
    }

    public void Respuesta()
    {
        respuesta.Invoke();
    }
}
