using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputCtrl : MonoBehaviour, IRayCastHit {

    private EnemyClassTable table;
    private UI_Manager UI_manager;
    private Vector3 bac;
    private void Awake()
    {
        table = GetComponent<EnemyClassTable>();
        UI_manager = GetComponent<UI_Manager>();
    }

    public void RayEnter()
    {
        if (table.model.IsDead()) return;
        if (table.rayChk.WallChk(this.transform.position.ToVector2IntXZ(),
            TurnManager.instacne.currentTurnUnit.transform.position.ToVector2IntXZ())) return;

        GameManager.instance.nodeColorChanger.IsChange = true;
        table.rim.IsChange = true;
        UI_Manager.instance.scope.IsScope = true;
        UI_Manager.instance.scope.scopeCanvas.transform.position = GetComponent<CharacterClassTable>().ui_bar.transform.position + new Vector3(0f, -0.43f, -0.28f);
    }
    public void RayExit()
    {

        GameManager.instance.nodeColorChanger.IsChange = false;
        table.rim.IsChange = false;
        UI_Manager.instance.scope.IsScope = false;
    }
    public void RayDown()
    {
        if (table.model.IsDead()) return;
        if (table.rayChk.WallChk(this.transform.position.ToVector2IntXZ(),
            TurnManager.instacne.currentTurnUnit.transform.position.ToVector2IntXZ())) return;
        CallBack.battle.Attack(this.gameObject);
    }
}