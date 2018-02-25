using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorSesion : MonoBehaviour
{
    public AjustesJuego ajustesJuego;
    public TodosMisEventos todosMisEventos;

    public static GestorSesion singleton;

    public bool estaListo;

    public void TomarEstadoListo(bool estado)
    {
        estaListo = estado;
    }
    private void Awake()
    {
        if(GestorSesion.singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);

            ajustesJuego.Init();
            ajustesJuego.gestorRecursos.Init();
            ajustesJuego.gestorSesion = this;
        }
        else
        {
            Debug.Log("Gestor sesion ha sido destruido");
            Destroy(this.gameObject);
        }
        
    }

    private void Start()
    {
        if (todosMisEventos.inicioJuego != null)
            todosMisEventos.inicioJuego.Subida();
    }

    public void CargarEscena(string nvl)
    {
        StartCoroutine(CargarEscenaAsyncActual(nvl));
    }

    public void CargarEscenaSola(string nvl)
    {
        StartCoroutine(CargarEscenaSolaActual(nvl));
    }

    public void CargarEscenaDesdeAjusteJuego()
    {
        CargarEscenaSola("Nivel1");
    }

    public void CargarNivelMultiPlayer()
    {
        //if(!estaListo)
            //return;
    }
    IEnumerator CargarEscenaAsyncActual(string nvl)
    {
        yield return SceneManager.LoadSceneAsync(nvl, LoadSceneMode.Additive);
        if (todosMisEventos.escenaAditiva != null)
        {
            todosMisEventos.escenaAditiva.Subida();
        }
    }

    IEnumerator CargarEscenaSolaActual(string nvl)
    {
        yield return SceneManager.LoadSceneAsync(nvl, LoadSceneMode.Single);
        yield return new WaitForSeconds(0.2f);
        if (todosMisEventos.escenaUno != null)
        {
            todosMisEventos.escenaUno.Subida();
        }
    }
}
