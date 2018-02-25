using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPanel : MonoBehaviour
{
    public RectTransform t;

    float ve;

    bool abrir;

    public void Abrir(float v)
    {
        ve = v;
        abrir = true;
    }

    void FixedUpdate()
    {
        if(abrir)
        {
            float newPos = Mathf.Lerp(t.sizeDelta.y, ve, 5 * Time.deltaTime);
            t.sizeDelta = new Vector2(t.sizeDelta.x, newPos);
        }     
    }

    IEnumerator Desactivar()
    {
        yield return new WaitForSeconds(1f);
        abrir = false;
    }
}
