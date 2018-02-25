using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementoUI : MonoBehaviour
{
    UIUpdate actualizador;

    private void Start()
    {
        actualizador = UIUpdate.singleton;

        if (actualizador != null)
            actualizador.elementos.Add(this);
        
    }
    public virtual void Init()
    {

    }

    public virtual void Tick(float delta)
    {

    }
}
