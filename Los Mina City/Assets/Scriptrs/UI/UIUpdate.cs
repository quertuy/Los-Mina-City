using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    public List<ElementoUI> elementos = new List<ElementoUI>();

    public static UIUpdate singleton;

    private void Awake()
    {
        singleton = this;
    }
    private void Update()
    {
        float delta = Time.deltaTime;

        for (int i = 0; i < elementos.Count; i++)
        {
            elementos[i].Tick(delta);
        }
    }
}
