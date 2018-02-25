using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorCamara : MonoBehaviour
{
	public Transform camTrans;
	public Transform objetivo;
	public Transform pivot;
    public Transform mTransform;
    public Bool pivotIzq;
    public Bool apuntando;
    public Bool agachado;
    float delta;

	float mouseX;
	float mouseY;
	float suavizadoX;
	float suavizadoY;
	float suavizadoXvelocidad;
	float suavizadoYvelocidad;
	float anguloMira;
	float anguloTilt;

	public ValoresCamara valores;

    public bool bloquearCursor;

	public void Init(Controlador controlador)
	{
		objetivo = controlador.gestorEstado.mTransform;

        mTransform = this.transform;

        if(bloquearCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}

	public void FixedTick(float d)
	{
		delta = d;

		if (objetivo == null)
			return;

        ManejarPosicion();
        ManejarRotacion();

        float vel = valores.velMovimiento;
        if (apuntando.valor)
            vel = valores.velApuntar;

        Vector3 posObjetivo = Vector3.Lerp(mTransform.position, objetivo.position, delta * vel);

        mTransform.position = posObjetivo;
	}

	void ManejarPosicion()
	{
		float objetivoX = valores.normalX;
		float objetivoY = valores.normalY;
		float objetivoZ = valores.normalZ;

		if (agachado.valor)
			objetivoY = valores.agachadoY;

		if(apuntando.valor)
		{
			objetivoX = valores.apuntarX;
			objetivoZ = valores.apuntarZ;
		}

		if (pivotIzq.valor)
			objetivoX = -objetivoX;

		Vector3 posNuevapivot = pivot.localPosition;
		posNuevapivot.x = objetivoX;
		posNuevapivot.y = objetivoY;

		Vector3 newCamPos = camTrans.localPosition;
		newCamPos.z = objetivoZ;

        float t = delta * valores.adaptVel;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posNuevapivot, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCamPos, t);
	}

    void ManejarRotacion()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (valores.suavizadoGiro > 0)
        {
            suavizadoX = Mathf.SmoothDamp(suavizadoX, mouseX, ref suavizadoXvelocidad, valores.suavizadoGiro);
            suavizadoY = Mathf.SmoothDamp(suavizadoY, mouseY, ref suavizadoYvelocidad, valores.suavizadoGiro);
        }
        else
        {
            suavizadoX = mouseX;
            suavizadoY = mouseY;
        }

        anguloMira += suavizadoX * valores.velRotY;
        Quaternion rotObjetivo = Quaternion.Euler(0, anguloMira, 0);
        mTransform.rotation = rotObjetivo;

        anguloTilt -= suavizadoY * valores.velRotX;
        anguloTilt = Mathf.Clamp(anguloTilt, valores.anguloMin, valores.anguloMax);
        pivot.localRotation = Quaternion.Euler(anguloTilt, 0, 0);
    }
}
