using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    Animator anim;
    GestorEstado gestorEstado;

    float longitudM;
    float longitudO;
    float longitudL;
    float longitudB;

    public Transform manoIzq;
    Transform manoDer;
    public Transform hombro;
    public Transform pivotApuntar;

    Vector3 dirMira;

    public bool desactivarO;
    public bool desactivarM;

    public ArmaRuntime armaActual;


    public void Init(GestorEstado ge)
    {
        gestorEstado = ge;
        anim = gestorEstado.anim;

       // hombro = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform;
        pivotApuntar = new GameObject().transform;
        pivotApuntar.name = "pivot Apuntar";
        pivotApuntar.transform.parent = gestorEstado.transform;
        manoDer = new GameObject().transform;
        manoDer.name = "ManoDer";
        manoDer.parent = pivotApuntar;
        gestorEstado.entradas.posApuntar = gestorEstado.transform.position +  transform.forward * 15;
        gestorEstado.entradas.posApuntar.y += 1.4f;
    }

    public void ArmaEquipada(ArmaRuntime ar)
    {
        Arma a = ar.armaActual;
        manoIzq = ar.armas.manoIzq;

        manoDer.localPosition = a.posicionIK.pos;
        manoDer.localEulerAngles = a.posicionIK.rot;
        basePos = a.posicionIK.pos;
        baseRot = a.posicionIK.rot;
        armaActual = ar;
    }
    private void OnAnimatorMove()
    {
        dirMira = gestorEstado.entradas.posApuntar - pivotApuntar.position;
        ManejarHombro();
    }

    void ManejarHombro()
    {
        ManejarPosHombro();
        ManejarRotHombro();
    }

    void ManejarPosHombro()
    {
        pivotApuntar.position = hombro.position;
    }

    void ManejarRotHombro()
    {
        Vector3 dirObjetivo = dirMira;
        if (dirObjetivo == Vector3.zero)
            dirObjetivo = pivotApuntar.forward;
        Quaternion tr = Quaternion.LookRotation(dirObjetivo);
        pivotApuntar.rotation = Quaternion.Slerp(pivotApuntar.rotation, tr, gestorEstado.delta * 15);
    }

    void ManejarLongitud()
    {
        if(gestorEstado.estados.interactuando)
        {
            longitudM = 0;
            longitudO = 0;
            longitudL = 0;
            return;
        }

        float tLongitud = 0;
        
        float mLongitud = 0;

        if(gestorEstado.estados.apuntando)
        {
            mLongitud = 1;
            longitudB = .4f;
        }
        else
        {
            longitudB = .3f;
        }

        if (desactivarM)
            mLongitud = 0;

        if (manoIzq)
        {
            longitudO = 1;
        }
        else
            longitudO = 0;

        if (desactivarO)
            longitudO = 0;

        Vector3 dm = gestorEstado.entradas.posApuntar - gestorEstado.mTransform.position;
        float angulo = Vector3.Angle(gestorEstado.mTransform.forward, dm);
        if (angulo < 76)
            tLongitud = 1;
        else
            tLongitud = 0;

        if (angulo > 45)
            mLongitud = 0;

        longitudL = Mathf.Lerp(longitudL, tLongitud, gestorEstado.delta * 3);
        longitudM = Mathf.Lerp(longitudM, mLongitud, gestorEstado.delta * 9);

    }

    private void OnAnimatorIK()
    {
        ManejarLongitud();

        anim.SetLookAtWeight(longitudL, longitudB, 1,1, 1);
        anim.SetLookAtPosition(gestorEstado.entradas.posApuntar);

        if(manoIzq)
        {
            UpdateIK(AvatarIKGoal.LeftHand, manoIzq, longitudO);
        }

        UpdateIK(AvatarIKGoal.RightHand, manoDer, longitudM);
    }

    void UpdateIK(AvatarIKGoal goal, Transform t, float l)
    {
        anim.SetIKPositionWeight(goal, l);
        anim.SetIKRotationWeight(goal, l);
        anim.SetIKPosition(goal, t.position);
        anim.SetIKRotation(goal, t.rotation);
    }

    public void Tick()
    {
        RetrocesoActual();
    }

    float retrocesoT;
    Vector3 offsetPos;
    Vector3 offsetRot;
    Vector3 basePos;
    Vector3 baseRot;
    bool retrocediendo;

    public void RetrocesoAnim()
    {
        if(!retrocediendo)
        {
            retrocediendo = true;
            retrocesoT = 0;
            offsetPos = Vector3.zero;

        }
    }

    public void RetrocesoActual()
    {
        if(retrocediendo)
        {
            retrocesoT += gestorEstado.delta * 3;
            if(retrocesoT > .5f)
            {
                retrocesoT = 1f;
                retrocediendo = false;
            }

            offsetPos = Vector3.forward * gestorEstado.gestorArmas.Tomar().armaActual.retrocesoZ.Evaluate(retrocesoT);
            offsetRot = Vector3.right * 90 * -gestorEstado.gestorArmas.Tomar().armaActual.retrocesoY.Evaluate(retrocesoT);

            manoDer.localPosition = basePos + offsetPos ;
            manoDer.localEulerAngles = baseRot + offsetRot;
        }
    }
}
