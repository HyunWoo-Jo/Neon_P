
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UI_Attack : ButtonLight
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnButton();
        }
    }
    public override void OnButton()
    {
        if (ReturnChk()) return;
        if (!GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.Attack))
        {
            ui_button.reset_listener -= ResetUI;
            ui_button.ResetUIButton();
            ui_button.reset_listener += ResetUI;
            lighting.SetActive(true);
            icon.color = Color.gray;
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Attack;
            ui_button.InputButton();
        }
        else
        {
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
            ResetUI();
        }
    }
    protected override bool ReturnChk()
    {
        if (ui_button.currentButtonState.Equals(UI_Button.ButtonState.Notting)) return true;
        if (!ui_button.currentButtonState.Equals(UI_Button.ButtonState.Attack) &&
            !ui_button.currentButtonState.Equals(UI_Button.ButtonState.All)) return true; 
        if (GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.EnemyTurn)) return true;
        return false;
    }


}
