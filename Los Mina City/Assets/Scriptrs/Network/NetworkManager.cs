using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.PunBehaviour
{
    public PhotonLogLevel nivelLog = PhotonLogLevel.Informational;

    public string version;
    public TodosMisEventos todosMisEventos;
    public Bool conectado;
    public AjustesJuego ajustesJuego;
    public ReferenciasNetwork referenciasNetwork;
    public Transform posicionLocal;
    [HideInInspector]
    public GestorEstadoNetwork estadoNetwork;

    public EstadoMultiplayer estadoMultiplayer;

    public static NetworkManager singleton;

    private void Awake()
    {
        if (NetworkManager.singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }      
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Init();
        ConnectToServer();
    }

    public void Init()
    {
        conectado.valor = false;
        PhotonNetwork.logLevel = nivelLog;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = false;
    }

	public void ConnectToServer()
    {
        referenciasNetwork.Init(this);
        conectado.valor = false;
        PhotonNetwork.ConnectUsingSettings(version);
        todosMisEventos.falloConeccion.Subida();
    }

    //Llamadas Photon
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        conectado.valor = true;
        todosMisEventos.conectarAServidor.Subida();
        
    }

    public override void OnDisconnectedFromPhoton()
    {
        base.OnConnectedToMaster();
        conectado.valor = false;
    }

    public void OnApplicationQuit()
    {
        conectado.valor = false;
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Creo");
        Debug.Log(PhotonNetwork.isMasterClient);
        base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Entro");
        todosMisEventos.entrarSala.Subida();
        if (PhotonNetwork.isMasterClient)
        {
            todosMisEventos.esMasterClient.Subida();
        }
        InitMultiplayer();
        InstanciarControlador();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
        Debug.Log("Conectado Jugador");
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonCreateRoomFailed(codeAndMsg);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        referenciasNetwork.RemoverJugador(otherPlayer.ID);
    }

    public void PresionadoBotonListo()
    {
        //Debug.Log("J");
        if (PhotonNetwork.isMasterClient)
        {
            estadoNetwork.EmisionInicioJuego();
        }
        else
        {
            todosMisEventos.listo.Subida();
        }
    }

    public void InitMultiplayer()
    {
        GameObject go;
        go = PhotonNetwork.Instantiate("TiroteoPrefab", Vector3.zero, Quaternion.identity, 0);
        DontDestroyOnLoad(go);
        //go.transform.parent = referenciasNetwork.padreReferencias.transform;
        estadoNetwork = go.GetComponent<GestorEstadoNetwork>();
    }
    public void InstanciarControlador()
    {
        PerfilJugador p = ajustesJuego.perfilJugador;
        object[] objs = new object[7];
        objs[0] = p.nombreUsuario;
        objs[1] = p.equipo.valor;
        objs[2] = p.marcara.valor;
        objs[3] = p.armaPrincipal.valor;
        objs[4] = p.armaSecundaria.valor;
        objs[5] = p.esMujer.valor;
        objs[6] = PhotonNetwork.player.ID;

        ajustesJuego.jugadorLocalObj = PhotonNetwork.Instantiate("Controller", Vector3.zero, Quaternion.identity, 0, objs);
    }

    public void EntrarEnSaloDesdeAjustesJuego()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = (byte)20;

        PhotonNetwork.JoinOrCreateRoom("Prueba", ro, TypedLobby.Default);
    }
}
