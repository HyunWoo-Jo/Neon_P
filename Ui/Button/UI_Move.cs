using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Move : MonoBehaviour
{
    private UI_Button ui_button;
    bool isMove = false;
    bool IsMove
    {
        get { return isMove; }
        set
        {
            isMove = value;
            moveButton.SetActive(!value);
        }
    }
    private void OnEnable()
    {
        lighting.SetActive(false);
    }
    bool isAction = false;
    bool isState = false;
    [SerializeField]
    private GameObject moveButton;
    [SerializeField]
    private GameObject lighting;
    [SerializeField]
    private Image icon;
    private void Awake()
    {
        ui_button = GetComponentInParent<UI_Button>();
        ui_button.reset_listener += ResetUI;
    }
    private void ResetUI()
    {
        lighting.SetActive(false);
        icon.color = Color.white;
        NodeCreate.instance.NodeDelete();
        // S
    }
    

    private void Start()
    {
        CallBack.battle.move_listener += Move;
        CallBack.turn.start_listener += ResetParamUI;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnMove();
        }
    }


    public void OnMove()
    {
        if (ReturnChk()) return;     
        if(!GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.Move))
        {
            ui_button.reset_listener -= ResetUI;
            ui_button.ResetUIButton();
            ui_button.reset_listener += ResetUI;

            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Move;
            GameObject obj = TurnManager.instacne.currentTurnUnit;
            CallBack.battle.NodeCreate(obj.transform.position, obj.GetComponent<CharacterModel>().getMoveArea());
            isAction = true;
            Invoke("RecoverTime", 0.5f);
            lighting.SetActive(true);
            icon.color = Color.gray;
            ui_button.InputButton();
        } else
        {
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
            NodeCreate.instance.NodeDelete();
            isAction = true;
            Invoke("RecoverTime", 0.5f);
            ResetUI();
        }

    }
    private bool ReturnChk()
    {
        if (isAction) return true;
        if (isMove) return true;
        if (ui_button.currentButtonState.Equals(UI_Button.ButtonState.Notting)) return true;
        if (!ui_button.currentButtonState.Equals(UI_Button.ButtonState.Move) &&
            !ui_button.currentButtonState.Equals(UI_Button.ButtonState.All)) return true;
        if (GameManager.instance.mouseCtrl.mouseType.Equals(MouseCtrl.MouseType.EnemyTurn)) return true;
        return false;
    }


    void Move(List<Vector2Int> vec)
    {
        if (TurnManager.instacne.turn.Equals(TurnManager.Turn.Player))
        {
            IsMove = true;
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
        }
    }
    void ResetParamUI()
    {
        IsMove = false;
    }
    void RecoverTime()
    {
        isAction = false;
    }
}
