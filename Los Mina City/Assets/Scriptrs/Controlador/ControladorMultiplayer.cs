using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMultiplayer : MonoBehaviour
{
    GestorEstado gestorEstado;

    public void Init(GestorEstado ge)
    {
        gestorEstado = ge;
    }

	void Update ()
    {
        gestorEstado.NetworTick();
	}
}
