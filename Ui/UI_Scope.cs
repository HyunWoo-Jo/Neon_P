using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scope : MonoBehaviour
{
    public Canvas scopeCanvas;
    private bool isScope = false;
    private Vector3 lookCamera;

    public bool IsScope
    {
        get { return isScope; }

        set { Scope(value); }
    }

    private void Scope(bool isScope)
    {
        this.transform.GetComponent<Canvas>().worldCamera = __CameraMng.Instance.m_mainCamera.GetComponent<Camera>();
        lookCamera = Camera.main.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookCamera);
        transform.rotation = rot;
        if (isScope == true)
        {
            scopeCanvas.gameObject.SetActive(true);
        }
        else
        {
            scopeCanvas.gameObject.SetActive(false);
        }
    }
}
