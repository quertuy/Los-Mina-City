using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
	float horizontal;
	float vertical;

	bool apuntar;
	bool correr;
	bool disparar;
	bool agacharse;
	bool recargar;
	bool cambiar;
	bool pivot;
    bool saltar;

 

	public bool inicio;

	float delta;

	public GestorEstado gestorEstado;
    public GestorCamara gestorCamara;
    public ReferenciasJugador referenciasJugador;
    public AjustesJuego ajustesJuego;

    bool updateUI;

    void Awake()
	{
        if(ajustesJuego == null)
             ajustesJuego = Resources.Load("Ajustes Juego") as AjustesJuego;

        ajustesJuego.gestorRecursos.Init();

        if(NetworkManager.singleton == null)
        {
            InitInGame();
        }
        
	}

    public void InitInMultiplayer()
    {
        gestorEstado = ajustesJuego.jugadorLocalObj.GetComponent<GestorEstado>();

        GestorNivel gn = GestorNivel.singleton;
        Transform spawn = gn.TomarPosicionSpwan();

        gestorEstado.mTransform.position = spawn.position;
        gestorEstado.mTransform.rotation = spawn.rotation;
        gestorCamara.transform.position = spawn.position;
        gestorCamara.transform.rotation = spawn.rotation;

        referenciasJugador.Init();
        gestorCamara.Init(this);

        UpdateReferenciasJugadorParaArmas(gestorEstado.gestorArmas.Tomar());

        gestorEstado.rigid.isKinematic = false;

        //GestorEstadoNetwork.singleton.EntroEnJuego(gestorEstado.photonID);

        updateUI = true;
        inicio = true;
    }

	public void InitInGame()
	{
        referenciasJugador.Init();
        if (gestorEstado.gestorRecursos == null)
        {
            gestorEstado.gestorRecursos = ajustesJuego.gestorRecursos;
            ajustesJuego.gestorRecursos.Init();
        }
        gestorEstado.CargarPerfil(ajustesJuego.perfilJugador);
		gestorEstado.Init ();
        gestorCamara.Init(this);
		
        UpdateReferenciasJugadorParaArmas(gestorEstado.gestorArmas.Tomar());

        updateUI = true;
        inicio = true;

        Time.timeScale = 1;
	}

	void FixedUpdate()
	{
		if (!inicio)
			return;

		delta = Time.fixedDeltaTime;
		TomarEntradas ();
		EnJuego_UpdateEstados ();
		gestorEstado.FixedTick (delta);

        gestorCamara.FixedTick(delta);

        if (gestorEstado.rigid.velocity.sqrMagnitude > 0.5f)
            referenciasJugador.extencionObjetivo.valor += 5f;
    }

	void TomarEntradas()
	{
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
	}

	void EnJuego_UpdateEstados()
	{
		gestorEstado.entradas.horizontal = horizontal;
		gestorEstado.entradas.vertical = vertical;

		gestorEstado.entradas.cantidadMovimiento = Mathf.Clamp01 (Mathf.Abs(horizontal) + Mathf.Abs(vertical));
		Vector3 dirMov = gestorCamara.mTransform.forward * vertical;
		dirMov += gestorCamara.mTransform.right * horizontal;
		dirMov.Normalize ();
		gestorEstado.entradas.direccionMovimiento = dirMov;

        gestorEstado.entradas.direccionRotacion = gestorCamara.transform.forward;

      
    }

	void Update ()
	{
		if (!inicio)
			return;

		delta = Time.deltaTime;
        TomarEntradas_Update();
        PosicionApuntar();
        EnJuegoUpdateEstados_Update();
		gestorEstado.Tick (delta);

        if(updateUI)
        {
            updateUI = false;
            UpdateReferenciasJugadorParaArmas(gestorEstado.gestorArmas.Tomar());
            referenciasJugador.updateUI.Subida();
        }
	}

    public bool debugMira;
    void TomarEntradas_Update()
    {

        apuntar = Input.GetMouseButton(1);
        disparar = Input.GetMouseButton(0);
        correr = Input.GetKey(KeyCode.LeftShift);
        agacharse = Input.GetKeyDown(KeyCode.C);
        saltar = Input.GetKeyDown(KeyCode.Space);
        pivot = Input.GetKeyDown(KeyCode.V);
        recargar = Input.GetKeyDown(KeyCode.R);
    }

    void EnJuegoUpdateEstados_Update()
    {

        if(recargar)
        {
            bool recargando = gestorEstado.Recargar();

            if(recargando)
            {
                apuntar = false;
                disparar = false;
                updateUI = true;
            }
        }
        if(!debugMira)
            gestorEstado.estados.apuntando = apuntar;

        if (disparar)
        {
            gestorEstado.estados.apuntando = true;
            bool disparoActual = gestorEstado.DispararArma(Time.realtimeSinceStartup);

            if(disparoActual)
            {
                referenciasJugador.extencionObjetivo.valor = 80f;
                updateUI = true;
            }
        }

        referenciasJugador.apuntando.valor = gestorEstado.estados.apuntando;

        if (pivot)
            referenciasJugador.pivotIzq.valor = !referenciasJugador.pivotIzq.valor;

        if(agacharse)
           referenciasJugador.agachado.valor = !referenciasJugador.agachado.valor;
        gestorEstado.estados.agachado = referenciasJugador.agachado.valor;

        gestorEstado.estados.saltar = saltar;
        gestorEstado.estados.corriendo = correr;
    }

    void PosicionApuntar()
    {
        Ray ray = new Ray(gestorCamara.camTrans.position, gestorCamara.camTrans.forward);
        gestorEstado.entradas.posApuntar = ray.GetPoint(30);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, gestorEstado.ignorarCapas))
        {
            gestorEstado.entradas.posApuntar = hit.point;
        }
    }

    public void UpdateReferenciasJugadorParaArmas(ArmaRuntime r)
    {
        referenciasJugador.municion.valor = r.municion;
        referenciasJugador.cargador.valor = r.cargador;
    }
}

public enum FaseJuego
{
	enJuego, enMenu, enInventario
}
