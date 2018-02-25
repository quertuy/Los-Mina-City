using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/ReferenciasRuntime")]
public class ReferenciasRuntime : ScriptableObject
{
    public List<ArmaRuntime> armasRuntime = new List<ArmaRuntime>();

    public void Init()
    {
        armasRuntime.Clear();
    }

    public ArmaRuntime ArmaAArmaRuntime(Arma a)
    {
        ArmaRuntime ar = new ArmaRuntime();
        ar.armaActual = a;
        ar.municion = a.municion;
        ar.cargador = a.municionMaxima;
        armasRuntime.Add(ar);

        return ar;
    }

    public void QuitarArmaRuntime(ArmaRuntime ar)
    {
        if (ar.instancia)
            Destroy(ar.instancia);

        if (armasRuntime.Contains(ar))
            armasRuntime.Remove(ar);
    }
}

[System.Serializable]
public class ArmaRuntime
{
    public int municion;
    public int cargador;
    public float ultimoDisparo;
    public GameObject instancia;
    public Armas armas;
    public Arma armaActual;

    public void DispararArma()
    {
        armas.Disparo();
        municion--;

        if(armaActual.sonidoArma.disparando != null)
        {
            armas.Sonido(armaActual.sonidoArma.disparando);
        }
    }
}
