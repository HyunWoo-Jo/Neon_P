using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn
{
    public class SequenceSortState : TurnStateParent
    {
        public override void Enter()
        {
            StartCoroutine(SequenceSort());
        }
        public override void Exit()
        {

        }

        IEnumerator SequenceSort()
        {
            List<GameObject> sequence = new List<GameObject>();

            while (Stage.Instance == null)
            {
                yield return null;
                Debug.Log("대기");
            }
            Dictionary<GameObject, CharacterModel> dic = new Dictionary<GameObject, CharacterModel>();
            foreach (var item in Stage.Instance.unitData.unitDic)
            {
                dic.Add(item.Key, item.Value);
            }
            // 조건 검사해서 삭제
            // dic.Remove 조건검사 => 범위에 안 들어오는 에들이면 삭제;
            int count = dic.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject obj = null;
                int maxValue = 0;
                foreach (var item in dic)
                {
                    if (item.Value.getAgility() > maxValue)
                    {

                        maxValue = item.Value.getAgility();
                        obj = item.Key;
                    }
                }
                if (obj != null)
                {
                    dic.Remove(obj);
                    sequence.Add(obj);
                }
            }
            if (sequence.Count != 0)
            {

                owner.sequenceList = sequence;
                UI_Manager.instance.timeline.TimeLineCreate(sequence);
                yield return null;
                owner.ChangeState<TurnChkState>();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(SequenceSort());
            }
            yield return null;
        }
    }
}
