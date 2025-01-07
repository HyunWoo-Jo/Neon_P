using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn.AI
{
    [RequireComponent(typeof(GunAction))]
    public class RangeState : BattleState
    {
        private GunAction gun;
        protected override void Awake()
        {
            base.Awake();
            
            gun = GetComponent<GunAction>();
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
            
        }
        public override void TurnStart()
        {
            isMove = false;
            isAction = false;
            ObjChk();
            owner.table.move.moveEnd_listener += AccuracyRateChk;
            CallBack.battle.attackMotionEnd_listener += AttackEnd;
            owner.table.attack.rotateEnd_listener += AttackAction;
            CallBack.turn.end_listener += TurnEnd;
        }
        public void TurnEnd(float a)
        {
            owner.table.move.moveEnd_listener -= AccuracyRateChk;
            CallBack.battle.attackMotionEnd_listener -= AttackEnd;
            owner.table.attack.rotateEnd_listener -= AttackAction;
            CallBack.turn.end_listener -= TurnEnd;
        }

        bool isMove = false;
        bool isAction = false;
        void ObjChk()
        {
            if (hateTarget == null)
            {
                CallBack.turn.End(0.1f);
                return;
            }
            //오브젝트면
            if (pathFinderA.rayChk.ObjectChk(transform.position.ToVector2IntXZ(), hateTarget.transform.position.ToVector2IntXZ(), 1f, 1 << 11))
            {
                AccuracyRateChk();
            }
            else
            {
                PathAndObjChk();
            }
        }
        void MoveEndAccuracyRateChk()
        {
            AccuracyRateChk();
            owner.table.move.moveEnd_listener -= AccuracyRateChk;
        }
        #region __Attack__
        void AccuracyRateChk()
        {
            //if( 명즁률 10 퍼 이상이면)
            
            if (this.transform.position.CompareGridPosValue(hateTarget.transform.position) <= 10)
            {
                if (isAction) return;
                Attack();
                isAction = true;
            }
            else
            {
                if (isMove)
                {   
                    CallBack.turn.End(1f);
                }
                else
                {
                    pathFinderA.PathFindObj(transform.position, owner.table.model.getMoveArea(), hateTarget.transform.position.ToVector2IntXZ());
                    MoveTarget();
                }
            }
            //else

        }
        void Attack()
        {
            owner.table.attack.AttackTarget(hateTarget);
        }
        void AttackAction()
        {
            gun.Fire(owner.table, hateTarget);
        }
        void AttackEnd()
        {
            CallBack.turn.End(1f);
        }
        #endregion

        void PathAndObjChk()
        {
            pathFinderA.PathFindObj(transform.position, owner.table.model.getMoveArea(), hateTarget.transform.position.ToVector2IntXZ());
            if (pathFinderA.objList.Count == 0)
            {
                MoveTarget();
            }
            else
            {
                if (!MoveObject())
                {
                    MoveTarget();
                }
            }
        }
        bool MoveObject()
        {
            isMove = true;
            for (int i = 0; i < pathFinderA.objList.Count; i++)
            {
                if (pathFinderA.objList[i].CompareGridPosValue(owner.transform.position.ToVector2IntXZ()) <= 3)
                {
                    owner.table.move.Move(pathFinderA.GetPath(pathFinderA.objList[i]));
                    return true;
                }
            }
            return false;
        }

        void MoveTarget()
        {
            isMove = true;
            pathFinderA.AStarPathFind(transform.position.ToVector2IntXZ(), hateTarget.transform.position.ToVector2IntXZ());
            if (pathFinderA.isNotChk)
            {
                CallBack.turn.End(1f);
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
}