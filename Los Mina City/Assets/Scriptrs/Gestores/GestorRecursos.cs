using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Instancias/Recursos")]
public class GestorRecursos : ScriptableObject
{
    public ReferenciasRuntime runtime;
    public Arma[] armas;
    Dictionary<string, int> dic = new Dictionary<string, int>();

    public ContenedorMalla[] contenedorMalla;
    Dictionary<string, int> dicMalla = new Dictionary<string, int>();

    public ObjetoPersonaje[] objetoPersonaje;
    Dictionary<string, int> dicObjeto = new Dictionary<string, int>();

    public Mascara[] mascaras;
    Dictionary<string, int> dicMascaras = new Dictionary<string, int>();

    public void Init()
    {
        Init_Armas();
        Init_ContenedorMalla();
    }

    void Init_Armas()
    {
        for (int i = 0; i < armas.Length; i++)
        {
            if (dic.ContainsKey(armas[i].nombre))
            {

            }
            else
            {
                dic.Add(armas[i].nombre, i);
            }
        }
    }

    public Arma TomarArma(string nombre)
    {
        Arma retVal = null;
        int id = -1;
        if(dic.TryGetValue(nombre, out id))
        {
            retVal = armas[id];
        }

        return retVal;
    }

    void Init_ContenedorMalla()
    {
        for (int i = 0; i < contenedorMalla.Length; i++)
        {
            if (dicMalla.ContainsKey(contenedorMalla[i].id))
            {

            }
            else
            {
                dicMalla.Add(contenedorMalla[i].id, i);
            }
        }
    }

    public ContenedorMalla TomarMalla(string id)
    {
        ContenedorMalla r = null;
        int indice = -1;
        if(dicMalla.TryGetValue(id, out indice))
        {
            r = contenedorMalla[indice];
        }

        return r;
    }

    void InitMascara()
    {
        for (int i = 0; i < mascaras.Length; i++)
        {
            if(dicMascaras.ContainsKey(mascaras[i].id))
            {

            }
            else
            {
                dicMascaras.Add(mascaras[i].id, i);
            }
        }
    }

    public Mascara TomarMascara(string id)
    {
        Mascara r = null;
        int indice = -1;
        if(dicMascaras.TryGetValue(id, out indice))
        {
            r = mascaras[indice];
        }

        return r;
    }
}
