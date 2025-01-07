using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Stay : MonoBehaviour
{
    bool isAction = false;
    UI_Button ui_button;
    
    private void Awake()
    {
        ui_button = GetComponentInParent<UI_Button>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnStay();
        }
    }
    private void Start()
    {
        CallBack.turn.start_listener += delegate () { isAction = true; Invoke("Recover", 0.5f); };
    }

    public void OnStay()
    {
        if (ReturnChk()) return;
        if (TurnManager.instacne.turn != TurnManager.Turn.Enemy)
        {
            if (!isAction)
            {
                isAction = true;
                UI_Manager.instance.UI_playerSet(false);
                NodeCreate.instance.NodeDelete();
                ui_button.InputButton();
                CallBack.turn.End(1f);
            }
        }
    }
    private bool ReturnChk()
    {
        if (ui_button.currentButtonState.Equals(UI_Button.ButtonState.Notting)) return true;
        if (!ui_button.currentButtonState.Equals(UI_Button.ButtonState.Stay) &&
               !ui_button.currentButtonState.Equals(UI_Button.ButtonState.All)) return true;
        return false;
    }
    void Recover()
    {
        isAction = false;
    }

}
