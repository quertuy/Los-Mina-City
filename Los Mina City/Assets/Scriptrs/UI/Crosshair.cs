using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : ElementoUI
{
    public Float extencionObjetivo;
    public float maxExtencion = 70f;
    public float extencionPorDefecto;
    public float velExtencion = 5f;

    float t;
    float extencionAct;

    public Partes[] partes;

    public override void Tick(float delta)
    {
        t = delta * velExtencion;
        extencionAct = Mathf.Lerp(extencionAct, extencionObjetivo.valor, t);

        for (int i = 0; i < partes.Length; i++)
        {
            Partes p = partes[i];
            p.trans.anchoredPosition = p.pos * extencionAct;
        }

        extencionObjetivo.valor = Mathf.Lerp(extencionObjetivo.valor, extencionPorDefecto, delta);
    }

    public void AnadirExtencion(float v)
    {
        extencionObjetivo.valor = v;
    }

    [System.Serializable]
    public class Partes
    {
        public RectTransform trans;
        public Vector2 pos;
    }
}
