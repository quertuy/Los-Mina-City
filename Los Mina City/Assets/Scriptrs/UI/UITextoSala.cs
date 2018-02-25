using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextoSala : MonoBehaviour
{
    public Text tipoModo;
    public Text nombreNivel;
    public Text usuariosActuales;
    public Text maxUsuarios;
    public Sala salaObjetivo;

    private void Start()
    {
        CargarSala(salaObjetivo);
    }
    public void CargarSala(Sala s)
    {
        salaObjetivo = s;
        tipoModo.text = s.tipo.ToString();
        nombreNivel.text = s.nivelObjetivo;
    }
}
