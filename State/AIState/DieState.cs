using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn.AI
{
    public class DieState : State
    {
        AIManager owner;
        private void Awake()
        {
            owner = GetComponent<AIManager>();
        }

        public override void Enter()
        {
            base.Enter();
            owner.table.anim.Die();
            for (int i = 0; i < TurnManager.instacne.sequenceList.Count; i++)
            {
                if (TurnManager.instacne.sequenceList[i].GetInstanceID() == this.gameObject.GetInstanceID())
                {
                    TurnManager.instacne.sequenceList.RemoveAt(i);
                    break;
                }
            }
            Stage.Instance.unitData.enemyUnitDic.Remove(this.gameObject);
            Stage.Instance.unitData.unitDic.Remove(this.gameObject);
            owner.table.model.setIsDead(true);
            var tr = transform.position.ToVector2IntXZ();
            Stage.Instance.Grid[tr.x][tr.y].isUse = false;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BoxCollider>());
            CallBack.battle.DieUnit();
            CallBack.battle.MonsterDie();

        }
        public override void Exit()
        {
            base.Exit();
        }

    }
}
