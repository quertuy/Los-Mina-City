using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerfilJugador
{
    public string nombreUsuario;
    public Int dinero;
    public StringVariable equipo;
    public StringVariable marcara;
    public StringVariable armaPrincipal;
    public StringVariable armaSecundaria;
    public Bool esMujer;

    public List<string> itemsComprados = new List<string>();

    public void AnadirItem(string id)
    {
        itemsComprados.Add(id);
    }

    public bool Comprado(string id)
    {
        return itemsComprados.Contains(id);
    }
}
