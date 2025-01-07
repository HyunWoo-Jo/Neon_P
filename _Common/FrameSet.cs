using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSet : MonoBehaviour
{
    [SerializeField]
    private int targetFrame;

    private void Awake()
    {
        Application.targetFrameRate = targetFrame;   
    }
}
