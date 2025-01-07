using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Turn.AI
{
    public class PeaceState : State
    {
        protected AIManager owner;
        protected TracePlayerUnit trace;
        protected virtual void Awake()
        {

            owner = GetComponent<AIManager>();
            trace = new TracePlayerUnit();
        }
        public override void Enter()
        {
            base.Enter();
            if (owner.table.model.getPatrolArea() > 0)
                StartCoroutine(TracePlayer());
            StartCoroutine(HitChk());
        }
        public override void Exit()
        {
            base.Exit();

            StopAllCoroutines();
        }
        IEnumerator HitChk()
        {

            while (true)
            {
                yield return null;
                if (owner.table.model.hateCharacters.Count != 0)
                {
                    StateChange();
                }

            }
        }

        IEnumerator TracePlayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject traceUnit = trace.TraceTargetInArea(this.gameObject, owner.table.model.getPatrolArea());
                if (traceUnit != null)
                {
                    if (!owner.table.model.hateCharacters.ContainsKey(traceUnit))
                    {
                        HateLevel hate = new HateLevel();
                        hate.setId(traceUnit.GetInstanceID());
                        hate.setHateLevel(5);
                        owner.table.model.hateCharacters.Add(traceUnit, hate);
                        StateChange();
                    }
                }
            }
        }
        void StateChange()
        {
            if (owner.table.model.IsHaveGun())
                owner.ChangeState<RangeState>();
            else
                owner.ChangeState<MeleeState>();
        }
    }
}