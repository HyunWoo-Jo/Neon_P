using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMask : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float tension = 0.1f;

    Renderer maskRender;
    Vector2 offset;
    Vector2 offsetBuffer;

    private bool isRun = true;
    public bool IsRun
    {
        get { return isRun; }
        set { isRun = value;
            if(!value)
            {
                ResetMask();
            }
        }
    }

    private void Awake()
    {
        maskRender = GetComponent<Renderer>();
    }
    private void Start()
    {
        CallBack.turn.start_listener += TurnStart;
    }

    private void TurnStart()
    {
        
        target = TurnManager.instacne.currentTurnUnit.transform;
        isRun = true;
    }
    
    private void LateUpdate()
    {
        if (isRun)
        {
            if (target != null)
            {
                Move();
            }
        }
    }

    private void ResetMask()
    {      
        maskRender.material.SetTextureOffset("_UnlitColorMap", Vector2.zero);
    }



    private void Move()
    {
        Vector2 vec = new Vector2(target.position.x, target.position.z);
        Vector2 camVec = new Vector2(transform.position.x, transform.position.z);
        offset = (camVec - vec) * tension;
        if(offsetBuffer != offset)
        {
            offsetBuffer = offset;
            maskRender.material.SetTextureOffset("_UnlitColorMap", offset);
        }

    }



}
