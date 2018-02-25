using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorEstadoNetwork : Photon.MonoBehaviour
{
    public static GestorEstadoNetwork singleton;
    public TodosMisEventos todosMisEventos;
    public AjustesJuego ajustesJuego;
    public ReferenciasNetwork referenciasNetwork;
    public EstadoMultiplayer estadoMultiplayer;

    #region Inicial

    private void Awake()
    {
        singleton = this;
        todosMisEventos = Resources.Load("Todos Mis Eventos") as TodosMisEventos;
        if (ajustesJuego == null)
            ajustesJuego = Resources.Load("Ajuste Juego") as AjustesJuego;
        referenciasNetwork = Resources.Load("Referencias Multiplayer") as ReferenciasNetwork;
    }
    private void Start()
    {
        Init();
    }
    
    
    public virtual void Init()
    {
        estadoMultiplayer = EstadoMultiplayer.lobbygameLobby;
    }
    #endregion

    #region Actualizar
    private void Update()
    {
        Tick();
    }

    public virtual void Tick()
    {

    }
    #endregion

    #region Eventos
    public virtual void IniciarPartida(string nivel)
    {
        ajustesJuego.gestorSesion.estaListo = true;
        todosMisEventos.listo.Subida();
        Debug.Log("Inicio Partida");
    }

    public virtual void TerminarPartida()
    {

    }

    public virtual void RespawnJugador(int photonID)
    {

    }

    public virtual void RespawnTodos()
    {

    }

    public void EntroEnJuego(int photonID)
    {
        photonView.RPC("RPC_EntroEnJuego",PhotonTargets.AllBuffered ,photonID);
    }
    #endregion

    #region MisLlamadas
    public virtual void EmisionInicioJuego()
    {
        Debug.Log("Emision Inicio Partida");
        string nivel = "Nivel1";
        estadoMultiplayer = EstadoMultiplayer.inGame;
        photonView.RPC("RPC_IniciarPartida", PhotonTargets.AllBuffered, nivel, estadoMultiplayer);
    }
    #endregion
  
    #region RPC
    [PunRPC]
    protected void RPC_IniciarPartida(string nivel, EstadoMultiplayer estado)
    {
        estadoMultiplayer = estado;
        IniciarPartida(nivel);
    }

    [PunRPC]
    protected void RCP_TermitarPartida()
    {

    }

    [PunRPC]
    protected void RPC_RespawnJugador()
    {

    }

    [PunRPC]
    protected void RPC_RespawnTodos()
    {
        

    }
    [PunRPC]
    protected void RPC_EntroEnJuego(int photonID)
    {
        ContenedorPersonaje personaje = referenciasNetwork.TomarPersonaje(photonID);
        if(personaje.instancia != null)
            personaje.instancia.SetActive(true);
    }
    #endregion
}
public enum EstadoMultiplayer
{
    lobbygameLobby, inGame
}
