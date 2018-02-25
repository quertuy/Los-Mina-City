using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdateTexto : MonoBehaviour
{
    public Text objetivo;

    public Float floatVariable;
    public Int intVariable;
    public StringVariable stringVariable;


    private void OnEnable()
    {
        if (objetivo == null)
            objetivo = GetComponentInChildren<Text>();
    }
    public void ActualizarFloatVariable()
    {
        objetivo.text = floatVariable.valor.ToString();
    }

    public void ActualizarFloat(float v)
    {
        objetivo.text = v.ToString();
    }
    public void ActualizarIntVariable()
    {
        objetivo.text = intVariable.valor.ToString();
    }
    public void ActualizarInt(int v)
    {
        objetivo.text = v.ToString();
    }

    public void ActualizarStringVariable()
    {
        objetivo.text = stringVariable.valor;
    }

    public void ActualizarString(string r)
    {
        objetivo.text = r;
    }
}
