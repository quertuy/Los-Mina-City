using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GestorArmas
{
    public string apNombre;
    public string asNombre;

    ArmaRuntime armaActual;

    public ArmaRuntime Tomar()
    {
        return armaActual;
    }

    public void Set(ArmaRuntime ar)
    {
        armaActual = ar;
    }
    public ArmaRuntime pArma;
    public ArmaRuntime sArma;

}
