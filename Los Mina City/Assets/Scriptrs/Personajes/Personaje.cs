using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public Animator anim;
    public string persona;

    public ObjetoPersonaje objPelo;
    public ObjetoPersonaje objOjos;
    public Mascara mascaraObj;
    public ObjetoPersonaje otro;

    GameObject pelo;
    GameObject ojos;
    GameObject mascara;
    public bool esMujer;
    public SkinnedMeshRenderer cuerpo;

    public List<GameObject> objetosInstanciados = new List<GameObject>();

    public GestorRecursos gestorRecursos;

    public void Init(GestorEstado ge)
    {
        gestorRecursos = ge.gestorRecursos;
        anim = ge.anim;
        CargarPersonaje();
       // pelo = CargarObjeto(objPelo);
       // ojos = CargarObjeto(objOjos);
       // mascara = CargarMascara(mascaraObj);
      //  CargarObjeto(otro);
    }

    public void CargarPersonaje()
    {
        ContenedorMalla c = gestorRecursos.TomarMalla(persona);
        CargarContenedorMalla(c);
    }

    public void CargarContenedorMalla(ContenedorMalla c)
    {
        cuerpo.sharedMesh =(esMujer)? c.mallaMujer : c.mallaHombre;
        cuerpo.material = c.material;
    }

    public GameObject CargarMascara(Mascara m)
    {
        //pelo.SetActive(m.pelo);
        //ojos.SetActive(m.ojos);

        return null;
       // return CargarObjeto(m.obj);
    }

    public GameObject CargarObjeto(ObjetoPersonaje o)
    {
        Debug.Log(o);
        Transform p = anim.GetComponent<Transform>().transform;
        GameObject prefab = (esMujer) ? o.m_prefab : o.h_prefab;
        GameObject go = Instantiate(prefab);
        go.transform.parent = p;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        objetosInstanciados.Add(go);

        return go;
        
    }
}
