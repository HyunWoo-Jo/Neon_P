using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn
{
    public class TurnState : TurnStateParent
    {


        private GameObject currentUnit;
        private IUnitTurn unitTurn;

        public override void Enter()
        {
            currentUnit = owner.sequenceList[0];
            owner.sequenceList.RemoveAt(0);
            unitTurn = currentUnit.GetComponent<IUnitTurn>();
            if (Stage.Instance.unitData.playerUnitDic.ContainsKey(currentUnit))
            {
                owner.turn = TurnManager.Turn.Player;
            }
            else
            {
                owner.turn = TurnManager.Turn.Enemy;
            }
            owner.currentTurnUnit = currentUnit;
            CallBack.turn.end_listener += EndTurn;
            StartCoroutine(StartTurn());
        }

        public override void Exit()
        {
            unitTurn.TurnEnd();

            CallBack.turn.end_listener -= EndTurn;

        }

        IEnumerator StartTurn()
        {
            yield return null;
            CallBack.turn.Start();
            // 카메라
            __CameraMng.Instance.setBattleState(currentUnit.transform);

            unitTurn.TurnStart();
        }

        void EndTurn(float delay)
        {
            Debug.Log(delay);
            StartCoroutine(NextTurn(delay));
        }

        IEnumerator NextTurn(float delay)
        {
            yield return new WaitForSeconds(delay);
            owner.ChangeState<TurnChkState>();
        }


    }
}