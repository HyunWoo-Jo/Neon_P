using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFX.SFX;

public class Z_PlayerUnit : StandardPlayerUnit
{
    public SFX_AreaProtectorController dron;

    protected override void TurnEnter()
    {
        base.TurnEnter();
    }

    protected override void TurnExit()
    {
        base.TurnExit();
    }

    protected override void MouseEnemyT(GameObject obj)
    {
        GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.EnemyTurn;
        NodeCreate.instance.NodeDelete();
        StartCoroutine(AttackAction(obj));
    }

    IEnumerator AttackAction(GameObject obj)
    {
        
        __CameraMng.Instance.setAttackState(this.transform);
        UI_Manager.instance.UI_playerSet(false);
        Vector3 pos = obj.transform.position;
        pos.y += 0.5f;
        GameObject targetPivot = new GameObject();
        targetPivot.transform.position = pos;
        dron._target = targetPivot;
        dron.IsRun = true;
        table.anim.Attack();
        dron.GetComponent<DronMove>().Attack();
        yield return new WaitForSeconds(0.5f);
        if(Random.Range(0,2)== 1)
        {
            __CameraMng.Instance.setAttackState(obj.transform);
            GameManager.instance.mask.IsRun = false;
        } else
        {
            //GameManager.instance.camPivot.SetTarget(this.transform);
        }
        yield return new WaitForSeconds(3f);

        dron.IsRun = false;
        dron.GetComponent<DronMove>().Standard();
        table.anim.End();
        CallBack.turn.End(1.5f);
        Destroy(targetPivot);
        dron._target = null;
        CallBack.battle.AttackMotionEnd();
        //GameManager.instance.camPivot.ResetTarget();
    }

    ///

    
}
