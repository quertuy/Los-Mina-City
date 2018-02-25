using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruir : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
