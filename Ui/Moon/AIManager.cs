using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turn.AI;
public class AIManager : StateMachine
{
    [HideInInspector]
    public EnemyClassTable table;
    private void Awake()
    {
        table = GetComponent<EnemyClassTable>();
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        if (table.model.pathUnit != null)
            ChangeState<PatrolState>();
        else
            ChangeState<StayState>();
    }

    public void BattleTurnStart()
    {
        if (currentState == GetComponent<BattleState>())
        {
            GetComponent<BattleState>().TurnStart();
        }
    }

}

