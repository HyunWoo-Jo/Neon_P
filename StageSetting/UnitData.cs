using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    /// <summary>
    /// 나중에 딕셔너리 제너릭 수정 **
    /// </summary>
    public Dictionary<GameObject, CharacterModel> unitDic;
    public Dictionary<GameObject, CharacterModel> playerUnitDic;
    public Dictionary<GameObject, AICharacterModel> enemyUnitDic;

}
