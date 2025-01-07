using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorData : MonoBehaviour
{
#if UNITY_EDITOR
    public Camera mainCam;
    public Material[] _material;
    public GameObject[] _unit;
#endif
}
