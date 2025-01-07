using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn.AI
{
    public class StayState : PeaceState
    {
        public override void Enter()
        {
            StartCoroutine(TurnEnd());
            base.Enter();
        }
        public override void Exit()
        {
            StopAllCoroutines();
            base.Exit();
        }
        IEnumerator TurnEnd()
        {
            yield return new WaitForSeconds(3f);
            while (true)
            {
                yield return new WaitForSeconds(2f);
                if(TurnManager.instacne.currentTurnUnit.GetInstanceID() == this.gameObject.GetInstanceID())
                {
                    CallBack.turn.End(1);
                }
            }

        }
    }
}