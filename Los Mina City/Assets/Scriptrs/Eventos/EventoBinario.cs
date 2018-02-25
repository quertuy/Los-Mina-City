using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventoBinario : MonoBehaviour
{
    public Bool objetivo;

    public UnityEvent verdadero;
    public UnityEvent falso;

    public void Subida()
    {
        if (objetivo.valor)
        {
            verdadero.Invoke();
        }
        else
            falso.Invoke();
    }
	
}
