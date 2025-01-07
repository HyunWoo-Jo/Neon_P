using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlayerUnit : IUnitTurn
{
    protected bool isEnd = true;

    protected PlayerClassTable table;
    protected GameObject target;

    protected virtual void Awake()
    {
        table = GetComponent<PlayerClassTable>();
    }
    protected virtual void Start()
    {
        table.move.moveStart_listener += delegate () { UI_Manager.instance.UI_playerSet(false); };
        table.move.moveEnd_listener += delegate () {
            if (!table.move.isSigleMove)
                UI_Manager.instance.UI_playerSet(true);
        };
        table.move.moveStart_listener += delegate () { table.rim.IsChange = false; };
        table.move.moveEnd_listener += delegate () { table.rim.IsChange = true; };
    }
    void SetTarget(GameObject obj)
    {
        target = obj;
    }
    protected override void TurnEnter()
    {
        if (!isEnd) return;
        isEnd = false;
        table.move.isSigleMove = false;
        GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
        CallBack.battle.move_listener += table.move.Move;
        CallBack.battle.attack_listener += table.attack.AttackTarget;
        CallBack.battle.attack_listener += MouseEnemyT;
        CallBack.battle.attack_listener += SetTarget;
        
        //코스트 변경
        UI_Manager.instance.UI_playerSet(true);
        table.rim.IsChange = true;
        SkillNodeCreater.instance.inputMouseDown_listener += table.skil.startSkill;
    }

    protected override void TurnExit()
    {
        if (isEnd) return;
        isEnd = true;
        table.rim.IsChange = false;
        CallBack.battle.move_listener -= table.move.Move;
        CallBack.battle.attack_listener -= table.attack.AttackTarget;
        CallBack.battle.attack_listener -= MouseEnemyT;
        CallBack.battle.attack_listener -= SetTarget;

        UI_Manager.instance.UI_playerSet(false);
        SkillNodeCreater.instance.inputMouseDown_listener -= table.skil.startSkill;
    }



    protected virtual void MouseEnemyT(GameObject obj)
    {
        GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.EnemyTurn;
        NodeCreate.instance.NodeDelete();
    }

}
