using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorEstado : Photon.MonoBehaviour
{
    public ControladorEstados estados;
    public VariableEntradas entradas;
    public GestorArmas gestorArmas;
    public GestorRecursos gestorRecursos;
	public ContorladoEstadisticas estadisticas;
    public Personaje personaje;
    public ReferenciasNetwork referenciasNetwork;
    public EventosJuego localJugador;
    public float vida;
    public int photonID;

	[System.Serializable]
	public class VariableEntradas
	{
		public float horizontal;
		public float vertical;
		public float cantidadMovimiento;
		public Vector3 direccionMovimiento;
		public Vector3 posApuntar;
        public Vector3 direccionRotacion;
        public float extencionObjetivo;
	}
	[System.Serializable]
	public class ControladorEstados
	{
		public bool enSuelo;
		public bool apuntando;
		public bool agachado;
		public bool corriendo;
        public bool saltar;
		public bool interactuando;
	}

    [Header("Referencias")]
	public Animator anim;
	public GameObject modeloActivo;
    public IK iK;
	public Rigidbody rigid;
	[HideInInspector]
	public Collider colisor;

	List<Collider> ragdollColisores = new List<Collider>();
	List<Rigidbody> ragdollRigids = new List<Rigidbody>();

	public LayerMask ignorarCapas;
	LayerMask ignorarSuelo;
    [HideInInspector]
    public Transform padreReferencia;
    [HideInInspector]
	public Transform mTransform;
	public Estado estado;

	public float delta;

    bool esLocal;

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (personaje == null)
            personaje = GetComponent<Personaje>();

        object[] objs = photonView.instantiationData;
        personaje.persona = (string)objs[1];
        personaje.esMujer = (bool)objs[5];
        personaje.mascaraObj = gestorRecursos.TomarMascara((string)objs[2]);
        gestorArmas.apNombre = (string)objs[3];
        gestorArmas.asNombre = (string)objs[4];
        photonID = (int)objs[6];

        esLocal = photonView.isMine;

        referenciasNetwork.EntroJugador(photonID,this.gameObject,esLocal);
        Init();
        mTransform.parent = referenciasNetwork.padreReferencias.transform;
        rigid.isKinematic = true;

        if(esLocal)
        {
            if(localJugador)
                 localJugador.Subida();
        }
        else
        {
            ControladorMultiplayer control = gameObject.AddComponent<ControladorMultiplayer>();
            control.Init(this);
            //gameObject.SetActive(false);
        }
    }
    public void CargarPerfil(PerfilJugador p)
    {
        gestorArmas.apNombre = p.armaPrincipal.valor;
        gestorArmas.asNombre = p.armaSecundaria.valor;

        personaje = GetComponent<Personaje>();
        personaje.esMujer = p.esMujer.valor;
        personaje.persona = p.equipo.valor;
        personaje.mascaraObj = gestorRecursos.TomarMascara(p.marcara.valor);
    }

	public void Init()
	{
        gestorRecursos.Init();
        mTransform = this.transform;
		SetupAnimator ();
		rigid = GetComponent<Rigidbody> ();
		rigid.isKinematic = false;
		rigid.drag = 4;
		rigid.angularDrag = 999;
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		colisor = GetComponent<Collider> ();
        personaje = GetComponent<Personaje>();

        SetupRagdoll ();

		ignorarCapas = ~(1 << 9);
		ignorarSuelo = ~(1 << 9 | 1 << 10);

        iK = modeloActivo.GetComponent<IK>();
        iK.Init(this);

        Init_GestorArmas();

        personaje.Init(this);

    }

	void SetupAnimator () 
	{
		if(modeloActivo == null)
		{
			anim = GetComponent<Animator> ();
			modeloActivo = anim.gameObject;
		}

		if (anim == null)
			anim = modeloActivo.GetComponent<Animator> ();

		anim.applyRootMotion = false;
			
	}

	void SetupRagdoll()
	{
		Rigidbody[] rigids = modeloActivo.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody r in rigids)
		{
			if(r == rigid)
				continue;

			Collider c = r.gameObject.GetComponent<Collider> ();
			c.isTrigger = true;
			ragdollRigids.Add (r);
			ragdollColisores.Add (c);
			r.isKinematic = true;
			r.gameObject.layer = 10;
		}
	}

	public void FixedTick(float d)
	{
		delta = d;

		switch (estado) 
		{
		case Estado.normal:
			estados.enSuelo = EnSuelo ();
                if (estados.apuntando)
                    MovimientoApuntando();
                else
                    Movimiento();

                Rotacion();
			break;
		case Estado.enAire:
			rigid.drag = 0;
			estados.enSuelo = EnSuelo ();
			break;
		case Estado.cubierto:
			break;
		case Estado.saltando:
			break;
		default:
			break;
		}
		
	}

	void Movimiento()
	{
		if (entradas.cantidadMovimiento > 0.05f || estados.enSuelo == false)
			rigid.drag = 0;
		else
			rigid.drag = 4;
		float vel = estadisticas.velMovimiento;
		if (estados.corriendo)
			vel = estadisticas.velCorrer;
		if (estados.agachado)
			vel = estadisticas.velAgachado;

		Vector3 dir = Vector3.zero;
		dir = mTransform.forward * (vel * entradas.cantidadMovimiento);
		rigid.velocity = dir;
	}

	void Rotacion()
	{
        if(!estados.apuntando)
            entradas.direccionRotacion = entradas.direccionMovimiento;
		Vector3 dirObjetivo = entradas.direccionRotacion;
		dirObjetivo.y = 0;

		if (dirObjetivo == Vector3.zero)
			dirObjetivo = mTransform.forward;

		Quaternion dirMira = Quaternion.LookRotation (dirObjetivo);
		Quaternion rotObjetivo = Quaternion.Slerp (mTransform.rotation, dirMira, estadisticas.velRotacion * delta);
	 
		mTransform.rotation = rotObjetivo;
	}

	void MovimientoApuntando()
	{
        float vel = estadisticas.velAnimacion;
        Vector3 dir = entradas.direccionMovimiento * vel;
        rigid.velocity = dir;
	}

    float rt;

	public void Tick(float d)
	{
		delta = d;
		switch (estado) 
		{
		case Estado.normal:
			estados.enSuelo = EnSuelo ();
			ManejarAnimacionesTodas ();
            iK.Tick();
            ManejarSalto();

                if(estados.interactuando)
                {
                    rt += delta;
                    if(rt > 3)
                    {
                        estados.interactuando = false;
                        rt = 0;
                    }
                }
			break;
		case Estado.enAire:
			estados.enSuelo = EnSuelo ();
			break;
		case Estado.cubierto:
			break;
		case Estado.saltando:
			break;
		default:
			break;
		}

        if (!estados.enSuelo)
            estado = Estado.enAire;
        else
            estado = Estado.normal;
	}

    void ManejarAnimacionesTodas()
    {

        anim.SetBool("Correr", estados.corriendo);
        anim.SetBool("Apuntar", estados.apuntando);
        anim.SetBool("Agacharse", estados.agachado);

        if (estados.apuntando)
        {
            ManejarAnimacionesApuntando();
        }
        else
        {
            ManejarAnimacionesNormales();
        }
    }

    void ManejarAnimacionesNormales()
    {
        if (entradas.cantidadMovimiento > 0.05f)
            rigid.drag = 0;
        else
            rigid.drag = 4;

        float anim_v = entradas.cantidadMovimiento;
        anim.SetFloat("Vertical", anim_v, .15f, delta);
        anim.SetBool("enAire", !estados.enSuelo);
        anim.SetBool("Saltar", saltando);
    }

    void ManejarAnimacionesApuntando()
    {
        float v = entradas.vertical;
        float h = entradas.horizontal;

        anim.SetFloat("Horizontal", h, .2f, delta);
        anim.SetFloat("Vertical", v, .2f, delta);
    }

    bool saltando = false;
    void ManejarSalto()
    {
        if (estados.saltar && !saltando && entradas.vertical > 0)
        {
            float fuerza = 4;
            estados.enSuelo = false;
            saltando = true;
            rigid.AddForce(Vector3.up * fuerza, ForceMode.Impulse);
            //StartCoroutine(Saltar());
            StartCoroutine(CerrarSalto());
        }
        else
            saltando = false;
    }

    public void Init_GestorArmas()
    {
        CrearArmaRuntime(gestorArmas.apNombre, ref  gestorArmas.pArma);
        ArmaQuipada(gestorArmas.pArma);
    }

    public void CrearArmaRuntime(string nombre, ref ArmaRuntime rga)
    {
        Arma a = gestorRecursos.TomarArma(nombre);
        ArmaRuntime p = gestorRecursos.runtime.ArmaAArmaRuntime(a);

        GameObject go = Instantiate(a.modelo);
        p.instancia = go;
        p.armaActual = a;
        p.armas = go.GetComponent<Armas>();
        go.SetActive(false);

        Transform pd = anim.GetBoneTransform(HumanBodyBones.RightHand).GetChild(5);
        go.transform.parent = pd;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;

        rga = p;
    }

    public void ArmaQuipada(ArmaRuntime ar)
    {
        ar.instancia.SetActive(true);
        iK.ArmaEquipada(ar);
        anim.SetFloat("TipoArma", ar.armaActual.tipoArma);
        gestorArmas.Set(ar);
    }

    public bool DispararArma(float t)
    {
        bool r = false;

        ArmaRuntime a = gestorArmas.Tomar();
        if(a.municion > 0)
        {
            if (t - a.ultimoDisparo > a.armaActual.radioDisparo)
            {
                a.ultimoDisparo = t;
                r = true;
                a.DispararArma();
                iK.RetrocesoAnim();
                LogicaDisparo(a);
            }
        }
       

        return r;
    }

    public void LogicaDisparo(ArmaRuntime a)
    {
        Vector3 origen = iK.pivotApuntar.position;
        origen += iK.pivotApuntar.forward * .5f;

        for (int i = 0; i < 1; i++)
        {
            Vector3 p = entradas.posApuntar;
            Vector3 offset = mTransform.forward;
            float del = entradas.extencionObjetivo;

            del *= 1f;
            
            offset.x = Random.Range(-del, del);
            offset.y = Random.Range(-del, del);
            offset.z = Random.Range(-del, del);

            Vector3 d = p - origen;
            
            bool golpeo = false;
            RaycastHit hit = Balistica.RaycastDisparo(origen, d, ref golpeo, ignorarCapas);
        }
    }

    public bool Recargar()
    {
        bool r = false;
        ArmaRuntime a = gestorArmas.Tomar();
        if(a.municion < a.armaActual.municion)
        {
            if(a.armaActual.municion <= a.cargador)
            {
                a.municion = a.armaActual.municion;
                a.cargador -= a.municion;
            }
            else
            {
                a.municion = a.cargador;
                a.cargador = 0;
            }
            r = true;
            anim.CrossFade("Recargar", 0.2f);
            estados.interactuando = true;

            if (a.armaActual.sonidoArma.recargando != null)
            {
                a.armas.Sonido(a.armaActual.sonidoArma.recargando);
            }
        }
        return r;
    }

    Vector3 ultimaPos;
    Quaternion ultimaRot;
    Vector3 ultimaDir;

    public void NetworTick()
    {
        delta = Time.deltaTime;
        if(referenciasNetwork.networkManager.estadoNetwork.estadoMultiplayer == EstadoMultiplayer.inGame)
            Prediccion();
    }

    public void Prediccion()
    {
        Vector3 posActual = mTransform.position;
        Quaternion rotActual = mTransform.rotation;

        float distancia = Vector3.Distance(ultimaPos, posActual);
        float angulo = Vector3.Angle(ultimaRot.eulerAngles, rotActual.eulerAngles);

        if (distancia > estadisticas.distanciaOla)
            mTransform.position = ultimaPos;
        if (angulo > estadisticas.anguloOla)
            mTransform.rotation = ultimaRot;

        posActual += ultimaDir;
        rotActual *= ultimaRot;

        Vector3 posObjetivo = Vector3.Lerp(posActual, ultimaPos, estadisticas.prediccionVel);
        mTransform.position = posObjetivo;

        Quaternion rotObjetivo = Quaternion.Slerp(mTransform.rotation, ultimaRot, estadisticas.prediccionVel);
        mTransform.rotation = rotObjetivo;
    }

    public void OnPhotonSerializeView(PhotonStream ps, PhotonMessageInfo info)
    {
        if (ps.isWriting)
        {
            ps.SendNext(mTransform.position);
            ps.SendNext(mTransform.rotation);
        }
        else
        {
            Vector3 posObjetivo = (Vector3)ps.ReceiveNext();
            Quaternion rotObjetivo = (Quaternion)ps.ReceiveNext();
            //Vector3 dirObjetivo = (Vector3)ps.ReceiveNext();
            RecibirPosicionRotacion(posObjetivo, rotObjetivo);
        }
        
    }

    void RecibirPosicionRotacion(Vector3 p, Quaternion r)
    {
        ultimaDir = p - ultimaPos;
        ultimaDir /= 10;

        if (ultimaDir.magnitude > estadisticas.umbralMovimiento)
            ultimaDir = Vector3.zero;

        Vector3 ultimoEuler = ultimaRot.eulerAngles;
        Vector3 nuevoEuler = r.eulerAngles;
        if(Quaternion.Angle(ultimaRot, r) < estadisticas.umbralAngulo)
        {
            ultimaRot = Quaternion.Euler((nuevoEuler - ultimoEuler) / 10);
        }
        else
        {
            ultimaRot = Quaternion.identity;
        }
        ultimaPos = p;
        ultimaRot = r;
    }
    public float minDist = .2f;
    public float maxDist = .5f;

    bool EnSuelo()
	{
        bool r = false;
		Vector3 origen = mTransform.position;
		origen.y += .6f;
		Vector3 dir = -Vector3.up;
		float dis = .7f;

        RaycastHit adelante;
        RaycastHit atras;

        float disObj = Mathf.Lerp(minDist, maxDist, entradas.cantidadMovimiento);
        Vector3 a = origen + mTransform.forward * disObj;
        Vector3 b = origen + mTransform.forward * -disObj;
        Vector3 posAdelante = mTransform.position;
        Vector3 posAtras = mTransform.position;

        Debug.DrawRay(a, dir * dis);
        if (Physics.Raycast(a, dir, out adelante, dis, ignorarSuelo))
        {
            r = true;
            posAdelante = adelante.point;
        }

        Debug.DrawRay(b, dir * dis);
        if (Physics.Raycast(b, dir, out atras, dis, ignorarSuelo))
        {
            r = true;
            posAtras = atras.point;
        }

        if(r)
        {
            float d = posAdelante.y - posAtras.y;
            if(d != 0)
            {
                d /= 2;
                Vector3 tp = mTransform.position;
                tp.y = d;
                mTransform.position = tp;
            }
        }
        return r;
	}

    IEnumerator CerrarSalto()
    {
        yield return new WaitForSeconds(.2f);
        estados.enSuelo = true;
        estado = Estado.normal;
    }
}

public enum Estado
{
	normal, enAire, cubierto, saltando
}
