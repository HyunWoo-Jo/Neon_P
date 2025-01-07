using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_UniqBehavior : ButtonLight
{
    [SerializeField]
    private GameObject skillP;
    protected override void Start()
    {
        base.Start();
        SkillNodeCreater.instance.inputMouseDown_listener += delegate (Vector3 pos,List<GameObject> objs) { ResetUI(); };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnButton();
        }
    }
    public override void OnButton()
    {
        if (ReturnChk()) return;
        if (!GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.Skil))
        {
            ui_button.reset_listener -= ResetUI;
            ui_button.ResetUIButton();
            ui_button.reset_listener += ResetUI;
            lighting.SetActive(true);
            icon.color = Color.gray;
            SkillModel model = TurnManager.instacne.currentTurnUnit.GetComponent<SkillModel>();
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Skil;
            GameManager.instance.outLineCreater.LineCreate(TurnManager.instacne.currentTurnUnit.transform.position,
                    model.GetComponent<CharacterModel>().getShootArea());
            if (model.IsArrSkill())
            {
                SkillNodeCreater.instance.Create(model.getSkillArr());
            }
            else
            {
                SkillNodeCreater.instance.SkillGenerate(Vector3.zero, new List<GameObject>());
            }
            
            ui_button.InputButton();

        }
        else
        {
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
            ResetUI();
        }
    }
    protected override void ResetUI()
    {
        base.ResetUI();
        GameManager.instance.outLineCreater.DeleteLine();
        SkillNodeCreater.instance.Delete();
        if (TurnManager.instacne == null) return;
        if (TurnManager.instacne.currentTurnUnit == null) return;
        SkillModel model = TurnManager.instacne.currentTurnUnit.GetComponent<SkillModel>();
        if (model == null)
        {
            skillP.gameObject.SetActive(false);
            return;
        }
        if (model.curSkillCount < 1)
            skillP.gameObject.SetActive(false);
        else
            skillP.gameObject.SetActive(true);

    }
    

    protected override bool ReturnChk()
    {
        if (ui_button.currentButtonState.Equals(UI_Button.ButtonState.Notting)) return true;
        if (!ui_button.currentButtonState.Equals(UI_Button.ButtonState.Skil) &&
            !ui_button.currentButtonState.Equals(UI_Button.ButtonState.All)) return true;
        if (GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.EnemyTurn)) return true;
        SkillModel model = TurnManager.instacne.currentTurnUnit.GetComponent<SkillModel>();
        if (model == null) return true;
        if (model.curSkillCount < 1) return true;
        return false;
    }

}
