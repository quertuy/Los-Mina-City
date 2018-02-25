using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/TodosMisEventos")]
public class TodosMisEventos : ScriptableObject
{
    public EventosJuego cerrarAnuniante;
    public EventosJuego abrirAnunciante;
    public EventosJuego conectarAServidor;
    public EventosJuego crearSala;
    public EventosJuego entrarSala;
    public EventosJuego falloConeccion;
    public EventosJuego enInventario;
    public EventosJuego inicioJuego;
    public EventosJuego escenaAditiva;
    public EventosJuego escenaUno;
    public EventosJuego cargarUnaEscena;
    public EventosJuego localPlayer;

    public EventosJuego jugar;
    public EventosJuego listo;
    public EventosJuego presionoListo;
    public EventosJuego esMasterClient;
    public EventosJuego EnPartidaMultiplayerEnJuego;
}
