using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Evento")]
public class EventosJuego : ScriptableObject
{
    List<EventosJuegoListener> listeners = new List<EventosJuegoListener>();

	public void Registrar(EventosJuegoListener l)
    {
        listeners.Add(l);
    }

    public void NoResgistrar(EventosJuegoListener l)
    {
        listeners.Remove(l);
    }

    public void Subida()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].Respuesta();
        }
    }
}
