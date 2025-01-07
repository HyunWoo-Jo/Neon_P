using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject unitParent;

    [SerializeField]
    private Vector2Int[] movePos;
    [SerializeField]
    private Vector2Int[] targetPos;

    [SerializeField]
    private GameObject[] tutorialTextImg;

    [SerializeField]
    private GameObject[] arrowPanel;
    [SerializeField]
    private GameObject canvasArrow;
    [SerializeField]
    private Transform cameraPos;

    [SerializeField]
    private GameObject[] addUnit;

    int count = 0;
    int panelNumber = 0;
    int nodeLayer = 0b1 << 9;
    int monsterLayer = 0b1 << 10;

    private void Start()
    {
        TutorialSequence();
        CallBack.turn.end_listener += TurnEnd;
    }

    

    void TurnEnd(float a)
    {
        StopAllCoroutines();
        tutorialTextImg[panelNumber].SetActive(false);
    }

    void TutorialSequence()
    {
        switch (count)
        {
            case 0:
                GameStart_S0();
                break;
            case 1:

                break;
        }
        count++;
    }
    void GameStart_S0()
    {
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Move;
        GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Move;
        GameManager.instance.mouseCtrl.SetTarget(this.gameObject);
        tutorialTextImg[0].SetActive(true);   
        panelNumber = 0;
        arrowPanel[0].SetActive(true);
        CallBack.battle.nodeCreate_listener += InputMoveButton_S1;
    }
    void InputMoveButton_S1(Vector3 pos, int cost)
    {
        NextPanelSet();
        CanvasMove(movePos[0], 2f);
        arrowPanel[0].SetActive(false);
        StartCoroutine(SetRayTarget(movePos[0], nodeLayer));
        CallBack.battle.nodeCreate_listener -= InputMoveButton_S1;
        CallBack.battle.move_listener += NodeMouseInput_S2;
    }
    void NodeMouseInput_S2(List<Vector2Int> list)
    {
        canvasArrow.SetActive(false);
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Notting;
        CallBack.battle.move_listener -= NodeMouseInput_S2;
        StopAllCoroutines();
        InputAttackButton_S3();
    }

    void InputAttackButton_S3()
    {
        arrowPanel[1].SetActive(true);
        NextPanelSet();
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Attack;
        StartCoroutine(SetRayTarget(targetPos[0], monsterLayer));
        UI_Manager.instance.button.inputButton_listener += InputAttckButtonChk_S4;

    }
    void InputAttckButtonChk_S4()
    {
        __CameraMng.Instance.setMainState(canvasArrow.transform);
        CanvasMove(targetPos[0], 3f);
        arrowPanel[1].SetActive(false);
        NextPanelSet();
        CallBack.turn.end_listener += CameraMove_S5;
        CallBack.battle.monsterDie_listener += MonsterDie;
        UI_Manager.instance.button.inputButton_listener -= InputAttckButtonChk_S4;

    }
    void MonsterDie()
    {
        canvasArrow.SetActive(false);
        CallBack.battle.monsterDie_listener -= MonsterDie;
    }
    void CameraMove_S5(float a)
    {
        if (TurnManager.instacne.turn.Equals(TurnManager.Turn.Player))
        {
            StartCoroutine(CameraMovePanel());
            CallBack.turn.end_listener -= CameraMove_S5;
        }
    }
    IEnumerator CameraMovePanel()
    {
        NextPanelSet();
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Notting;
        yield return new WaitForSeconds(10f);
        
        Move_S6();
    }
    void Move_S6()
    {
        arrowPanel[0].SetActive(true);
        CanvasMove(movePos[1], 2f);
        __CameraMng.Instance.setMainState(canvasArrow.transform);
        NextPanelSet();
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Move;
        GameManager.instance.mouseCtrl.SetTarget(this.gameObject);
        UI_Manager.instance.button.inputButton_listener += MoveInput;
    }
    void MoveInput()
    {
        arrowPanel[0].SetActive(false);
        StartCoroutine(SetRayTarget(movePos[1], nodeLayer));
        CallBack.battle.move_listener += Skil_S7;
        UI_Manager.instance.button.inputButton_listener -= MoveInput;
    }
    void Skil_S7(List<Vector2Int> list)
    {   
        canvasArrow.SetActive(false);
        NextPanelSet();
        StopAllCoroutines();
        StartCoroutine(SkilCameraMove());
        CallBack.battle.move_listener -= Skil_S7;

    } IEnumerator SkilCameraMove()
    {
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Notting;
        __CameraMng.Instance.setMainState(cameraPos);
        GameManager.instance.mask.IsRun = false;
        yield return new WaitForSeconds(5f);
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.Skil;  
        arrowPanel[2].SetActive(true);
        GameManager.instance.mask.IsRun = true;
        GameManager.instance.mouseCtrl.targetV = targetPos[1];
        __CameraMng.Instance.setBattleState(TurnManager.instacne.currentTurnUnit.transform);
        UI_Manager.instance.button.inputButton_listener += SkilButtonInput;
    }
    void SkilButtonInput()
    {
        CanvasMove(targetPos[1], 2f);
        arrowPanel[2].SetActive(false);
        UI_Manager.instance.button.inputButton_listener -= SkilButtonInput;
        CallBack.turn.end_listener += EndGame;
    }
    void EndGame(float x)
    {
        canvasArrow.SetActive(false);
        GameManager.instance.mouseCtrl.ResetTarget();
        UI_Manager.instance.button.currentButtonState = UI_Button.ButtonState.All;
        StartCoroutine(EndTutorial());
        CallBack.turn.end_listener -= EndGame;
    }
    IEnumerator EndTutorial()
    {
        tutorialTextImg[6].SetActive(false);
        tutorialTextImg[7].SetActive(true);
        AddUnit(0, targetPos[2]);
        //AddUnit(1, targetPos[3]);
        yield return new WaitForSeconds(40f);
        tutorialTextImg[panelNumber].SetActive(false);
    }

    void AddUnit(int index, Vector2Int pos)
    {
        GameObject obj = Instantiate(addUnit[index]);
        obj.transform.position = pos.ToVector3XZ(1.16f);
        Stage.Instance.Grid[pos.x][pos.y].isUse = true;
        Stage.Instance.unitData.unitDic.Add(obj, obj.GetComponent<CharacterModel>());
        Stage.Instance.unitData.playerUnitDic.Add(obj, obj.GetComponent<CharacterModel>());
    }

    void NextPanelSet()
    {
        tutorialTextImg[panelNumber].SetActive(false);
        panelNumber++;
        tutorialTextImg[panelNumber].SetActive(true);
    }

    private void CanvasMove(Vector2Int targetPos, float y)
    {
        canvasArrow.transform.position = targetPos.ToVector3XZ(y);
        canvasArrow.gameObject.SetActive(true);
    }

    IEnumerator SetRayTarget(Vector2Int movePos, int layer)
    {
        while (true)
        {
            Ray ray = new Ray(movePos.ToVector3XZ(6), Vector3.down);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 10f);
            if(Physics.Raycast(ray,out hit,10f,layer))
            {
                GameManager.instance.mouseCtrl.SetTarget(hit.collider.gameObject);
            }
            yield return null;
        }
    }


}
