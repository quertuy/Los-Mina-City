using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/MultiplayerReferencias")]
public class ReferenciasNetwork : ScriptableObject
{
    List<ContenedorPersonaje> personajes = new List<ContenedorPersonaje>();
    public ContenedorPersonaje personajeLocal;

    public GameObject padreReferencias;
    public TransformArray localPos;
    public TransformArray loobySpawnPos;

    [HideInInspector]
    public NetworkManager networkManager;

    public void Init(NetworkManager net)
    {
        networkManager = net;
        LimpierReferencias();
    }

    void LimpierReferencias()
    {
        if (padreReferencias)
            Destroy(padreReferencias);

        padreReferencias = new GameObject();
        padreReferencias.name = "Parent references";
        padreReferencias.AddComponent<NoDestruir>();

        //for (int i = 0; i < personajes.Count; i++)
        //{
        //    if (personajes[i].instancia)
        //        Destroy(personajes[i].instancia);
        //}

        personajes.Clear();
    }

    public void EntroJugador(int photonId, GameObject go, bool esLocal)
    {
        ContenedorPersonaje cp = new ContenedorPersonaje();
        cp.photonID = photonId;
        cp.instancia = go;
        cp.gestorEstado = go.GetComponent<GestorEstado>();
        personajes.Add(cp);
        if (esLocal)
        {
            personajeLocal = cp;
            go.transform.position = localPos.valor[0].position;
            go.transform.rotation = localPos.valor[0].rotation;
        }
        else
        {
            if(NetworkManager.singleton.estadoNetwork.estadoMultiplayer == EstadoMultiplayer.inGame)
            {
                go.SetActive(false);
            }
            else
            {
                if (loobySpawnPos.valor.Length > personajes.Count)
                {
                    go.transform.position = loobySpawnPos.valor[personajes.Count - 1].position;
                    go.transform.rotation = loobySpawnPos.valor[personajes.Count - 1].rotation;
                }
                else
                {
                    go.transform.position = -Vector3.one * 500;
                }
            }
         
        }
            
            
    }
    public ContenedorPersonaje TomarPersonaje(int id)
    {
        for (int i = 0; i < personajes.Count; i++)
        {
            if (personajes[i].photonID == id)
                return personajes[i];
        }

        return null;
    }

    public void RemoverJugador(int photonID)
    {
        ContenedorPersonaje cp = TomarPersonaje(photonID);
        if (cp.instancia)
            Destroy(cp.instancia);
        personajes.Remove(cp);
    }
}
[System.Serializable]
public class ContenedorPersonaje
{
    public int photonID;
    public GameObject instancia;
    public GestorEstado gestorEstado;
}