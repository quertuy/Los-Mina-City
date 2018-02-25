using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorNivel : MonoBehaviour
{
    public Transform padreSpawn;
    public Transform[] posicionesSpawn;

    public Transform TomarPosicionSpwan()
    {
        int r = Random.Range(0, posicionesSpawn.Length);
        return posicionesSpawn[r];
    }
    private void Awake()
    {
        singleton = this;
        posicionesSpawn = padreSpawn.GetComponentsInChildren<Transform>();
    }

    public static GestorNivel singleton;
}
