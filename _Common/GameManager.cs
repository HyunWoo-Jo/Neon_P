using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
        CallBack.Clear();
        mouseCtrl.ResetTarget();
        mouseCtrl.mouseType = MouseCtrl.MouseType.All;
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.All;
    }
    [HideInInspector]
    public bool isStop;

    public MouseCtrl mouseCtrl;
    public NodeColorChanger nodeColorChanger;
    public Transform campivot;
    public CameraMask mask;
    public OutLineCreater outLineCreater;
    public SubCamera subcam;

    private void Start()
    {
        CallBack.system.endload_listener += load;
        
        __CameraMng.Instance.Init();
        __CameraMng.Instance.setMainState(campivot);
 
    }

    private void load()
    {
        
        //__CameraMng.Instance.m_mainCamera.setTarget(this.gameObject.transform);
    }

}
