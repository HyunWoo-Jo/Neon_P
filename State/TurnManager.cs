using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turn;
public class TurnManager : StateMachine
{
    public static TurnManager instacne;
    public enum Turn
    {
        Player,
        Enemy
    }
    public Turn turn = Turn.Player;
    public List<GameObject> sequenceList = new List<GameObject>();
    public GameObject currentTurnUnit;

    public Damage damageBuffer;
    #region SigleTon
    private void Awake()
    {
        if (instacne == null)
        {
            instacne = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        instacne = null;
    }
    #endregion




    private void Start()
    {
        CallBack.battle.monsterDie_listener += GameEnd;

    }


    public void GameStart()
    {
        ChangeState<TurnChkState>();
    }
    public void GameEnd()
    {
        if (Stage.Instance.unitData.enemyUnitDic.Count == 0)
        {
            ChangeState<EndGameState>();
        }
    }
}
