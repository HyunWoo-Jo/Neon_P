using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn.AI
{
    public class BattleState : State
    {
        protected AIManager owner;
        protected PathFinderAI pathFinderA;

        protected GameObject hateTarget;

        protected virtual void Awake()
        {
            owner = GetComponent<AIManager>();
            pathFinderA = new PathFinderAI();
        }

        public override void Enter()
        {
            base.Enter();
            Stage.Instance.unitData.unitDic.Add(this.gameObject, owner.table.model);
            UI_Manager.instance.timeline.AddUnit(this.gameObject);
            StartCoroutine(HateTarget());
        }
        public override void Exit()
        {
            base.Exit();
            StopAllCoroutines();
            Stage.Instance.unitData.unitDic.Remove(this.gameObject);
            UI_Manager.instance.timeline.DeleteUnit(this.gameObject.GetInstanceID());
        }

        public virtual void TurnStart() { }

        IEnumerator HateTarget()
        {
            while (true)
            {
                int maxHate = 0;

                if (owner.table.model.hateCharacters.Count == 0)
                {
                    owner.ChangeState<StayState>();
                    yield break;
                }
                List<GameObject> objList = new List<GameObject>();
                foreach (var item in owner.table.model.hateCharacters)
                {
                    int hateLevel = item.Value.getHateLevel();
                    if (maxHate < hateLevel)
                    {
                        if (item.Key.GetComponent<CharacterModel>().IsDead())
                        {
                            objList.Add(item.Key);
                            continue;
                        }
                        maxHate = hateLevel;
                        hateTarget = item.Key;
                    }
                    
                }
                for (int i = 0; i < objList.Count; i++)
                {
                    owner.table.model.hateCharacters.Remove(objList[i]);
                }
                yield return null;
            }
        }
        private void HateAddAroundUnit()
        {
            foreach(var item in Stage.Instance.unitData.enemyUnitDic)
            {
                if (Vector3.Distance(transform.position, item.Value.transform.position) > 6f) return;
                AICharacterModel model = item.Value.GetComponent<AICharacterModel>();
                if (model.hateCharacters.Count == 0)
                {
                   
                }
            }
        }

    }
}
