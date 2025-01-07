using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turn.AI
{
    public class TracePlayerUnit
    {
        public GameObject TraceTargetInArea(GameObject owner, int cost)
        {
            GameObject InUnit = null;
            foreach (var item in Stage.Instance.unitData.playerUnitDic)
            {
                int compareGridPos = owner.transform.position.CompareGridPosValue(item.Key.transform.position);
                if (compareGridPos <= cost)
                {
                    InUnit = item.Key;
                    break;
                }
            }
            return InUnit;
        }
    }
}