using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyClassTable))]
public class StandardEnemy : IUnitTurn
{
    bool isEnd = true;
    EnemyClassTable table;
    private void Awake()
    {
        table = GetComponent<EnemyClassTable>();
    }

    protected override void TurnEnter()
    {
        if (!isEnd) return;
        isEnd = false;
        GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.EnemyTurn;
        CallBack.battle.move_listener += table.move.Move;
        UI_Manager.instance.UI_playerSet(false);
        table.rim.IsChange = true;
        __CameraMng.Instance.setMainState(this.transform);
        table.ai.BattleTurnStart();
    }
    protected override void TurnExit()
    {
        if (isEnd) return;
        isEnd = true;
        CallBack.battle.move_listener -= table.move.Move;
        table.rim.IsChange = false;
    }

}
