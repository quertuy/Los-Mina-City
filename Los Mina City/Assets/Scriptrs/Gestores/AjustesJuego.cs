using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/AjusteJuego")]
public class AjustesJuego : ScriptableObject
{
    public GestorRecursos gestorRecursos;
    public string version = "2.1";

    public bool conectado;
    public PerfilJugador perfilJugador;
    public PerfilJugador uiPerfilJugador;
    public GestorSesion gestorSesion;

    [HideInInspector]
    public GameObject jugadorLocalObj;

    public bool resetearValores;

    public UIAjustes uIAjustes;

    [System.Serializable]
    public class UIAjustes
    {
        public Sala salaActual;
        public EventosJuego salaCambiada;
        public StringVariable tipoSala;
        public StringVariable descripcionSala;
    }

    public void UpdateSalaActual(Sala objetivoSala)
    {
        uIAjustes.salaActual = objetivoSala;
    }
    public void Init()
    {
        if(resetearValores)
        {
            Debug.Log("Valores reseteados");
            PerfilPorDefecto();
        }
    }

    public void PerfilPorDefecto()
    {
        perfilJugador.itemsComprados.Clear();
        perfilJugador.AnadirItem("Nada");
        perfilJugador.AnadirItem("M4");
        perfilJugador.AnadirItem("Glock");
        perfilJugador.AnadirItem("Pelo");
        perfilJugador.AnadirItem("Gafas");
        perfilJugador.marcara.valor = "Ladron";
        perfilJugador.armaPrincipal.valor = "M4";
        perfilJugador.armaSecundaria.valor = "Glock";
        perfilJugador.equipo.valor = "Soldado";
        perfilJugador.dinero.valor = 10000;
    }
}
