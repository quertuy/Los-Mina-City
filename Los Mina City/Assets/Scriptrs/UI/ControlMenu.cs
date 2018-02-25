using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlMenu : MonoBehaviour
{
    public EventSystem eventSystem;

    public void CamiarPrimerBoton(GameObject go)
    {
        eventSystem.SetSelectedGameObject(go);
    }
}
