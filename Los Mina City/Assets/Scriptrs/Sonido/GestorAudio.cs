using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gestor Audio")]
public class GestorAudio : ScriptableObject
{
    public AudioClip[] clips;
    public float minTono = 1;
    public float maxTono = 1;

    public AudioClip TomarClip()
    {
        int ran = Random.Range(0, clips.Length);

        return clips[ran];
    }

    public float TomarTono()
    {
        return Random.Range(minTono, maxTono);
    }
}
