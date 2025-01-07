using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn.AI
{
    public class MeleeState : BattleState
    {
        protected override void Awake()
        {
            base.Awake();
            owner.table.attack.rotateEnd_listener += AttackAction;
        }

        public override void Enter()
        {
            base.Enter();
            owner.table.move.moveEnd_listener += Attack;
        }
        public override void Exit()
        {
            base.Exit();
            owner.table.move.moveEnd_listener -= Attack;
            StopAllCoroutines();
        }

        public override void TurnStart()
        {
            isEnd = false;
            TargetChk();
        }

        bool isEnd = false;

        void TargetChk()
        {
            if (hateTarget == null)
            {
                CallBack.turn.End(1.1f);
                return;
            }
            if (TargetInAttackRange())
            {//ATtack구현
                Attack();
            }
            else
            {
                pathFinderA.MoveAbleGridFind(transform.position, owner.table.model.getMoveArea());
                pathFinderA.AStarPathFind(transform.position.ToVector2IntXZ(), hateTarget.transform.position.ToVector2IntXZ());
                if (pathFinderA.isNotChk)
                {
                    Attack();
                    pathFinderA.isNotChk = false;
                    return;
                }
                for (int i = 0; i < pathFinderA.aStarPath.Count; i++)
                {
                    if (pathFinderA.closeNode.ContainsKey(pathFinderA.aStarPath[i]))
                    {
                        owner.table.move.Move(pathFinderA.GetPath(pathFinderA.aStarPath[i]));
                        break;
                    }
                }

            }
        }

        void Attack()
        {
            if (TargetInAttackRange())
            {//ATtack구현
                CallBack.battle.attackMotionEnd_listener += TurnEnd;
                owner.table.attack.AttackTarget(hateTarget);
            }
            else
            {
                CallBack.turn.End(1.2f);
            }

        }
        void AttackAction()
        {
            owner.table.anim.Attack();
            owner.table.sound.OneShot(0, 0.5f);
        }

        private void TurnEnd()
        {
            CallBack.turn.End(1.3f);
            CallBack.battle.attackMotionEnd_listener -= TurnEnd;
        }


        bool TargetInAttackRange()
        {
            if (owner.transform.position.CompareGridPosValue(hateTarget.transform.position) == 1)
            {
                return true;
            }
            return false;

        }

    }
}