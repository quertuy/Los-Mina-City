using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armas : MonoBehaviour
{
    public Transform manoIzq;

    ParticleSystem[] particulas;

    AudioSource sonidoDisparo;

    private void OnEnable()
    {
        particulas = transform.GetComponentsInChildren<ParticleSystem>();

        sonidoDisparo = GetComponent<AudioSource>();
    }
    public void Disparo()
    {
        for (int i = 0; i < particulas.Length; i++)
        {
            particulas[i].Play();
        }
    }

    public void Sonido(GestorAudio g)
    {
        sonidoDisparo.pitch = g.TomarTono();
        sonidoDisparo.PlayOneShot(g.TomarClip());
    }
}
