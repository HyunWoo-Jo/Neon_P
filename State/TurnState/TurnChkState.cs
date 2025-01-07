using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn
{
    public class TurnChkState : TurnStateParent
    {
        public override void Enter()
        {
            StartCoroutine(TurnChk());
        }
        public override void Exit()
        {

        }

        IEnumerator TurnChk()
        {
            //조건 
            yield return null;
            // 죽은유닛있나 확인
            if (false)
            {
                //있으면 딕서너리 제거
                int count = Stage.Instance.unitData.unitDic.Count;
                foreach (var item in Stage.Instance.unitData.unitDic)
                {
                    Debug.Log("?");
                    GameObject dicUnit = item.Key;
                    //if(사망일시)
                    Stage.Instance.unitData.unitDic.Remove(dicUnit);
                    if (Stage.Instance.unitData.playerUnitDic.ContainsKey(dicUnit))
                    {
                        Stage.Instance.unitData.playerUnitDic.Remove(dicUnit);
                    }
                    else if (Stage.Instance.unitData.enemyUnitDic.ContainsKey(dicUnit))
                    {
                        Stage.Instance.unitData.enemyUnitDic.Remove(dicUnit);
                    }
                }
            }

            if (owner.sequenceList.Count == 0)
            {
                owner.ChangeState<SequenceSortState>();
            }
            else
            {
                owner.ChangeState<TurnState>();
            }

        }
        void GameEnd()
        {
            // 조건체크해서 True false 반환
            if (true)
            {
                CallBack.system.GameEnd(true);

            }
        }



    }
}