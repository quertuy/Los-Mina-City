using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetardoEvento : MonoBehaviour
{
    public EventosJuego eventoJuego;
    bool esperando;

    public void SubirRetardo(float v)
    {
        if (eventoJuego == null)
            return;

        if (esperando)
            return;

        esperando = true;
        StartCoroutine(Retardo(v));

    }

    IEnumerator Retardo(float v)
    {
        yield return new WaitForSeconds(v);
        eventoJuego.Subida();
        esperando = false;
    }
}
