using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpdateSpawnPos : MonoBehaviour
{
    public Transform localSpawn;
    public TransformArray localSpawnVariable;
    
    public Transform padreClientes;
    public TransformArray clientesSpanwVariable;

    private void Start()
    {
        localSpawnVariable.valor = new Transform[1];
        localSpawnVariable.valor[0] = localSpawn;

        Transform[] cs = padreClientes.GetComponentsInChildren<Transform>();
        clientesSpanwVariable.valor = new Transform[cs.Length];
        for (int i = 0; i < cs.Length; i++)
        {
           
             clientesSpanwVariable.valor[i] = cs[i];
        }
    }
}
