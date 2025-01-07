using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ray : MonoBehaviour
{
    private void OnEnable()
    {
        if(GameManager.instance != null)
            GameManager.instance.mouseCtrl.isRun = true;
    }

    private void OnDisable()
    {
        GameManager.instance.mouseCtrl.isRun = false;
    }
    public void Enter()
    {
        GameManager.instance.mouseCtrl.isRun = false;
    }
    public void Exit()
    {
        GameManager.instance.mouseCtrl.isRun = true;
    }
}
