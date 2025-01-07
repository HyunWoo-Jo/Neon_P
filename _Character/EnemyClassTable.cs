using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turn.AI;
[RequireComponent(typeof(AICharacterModel))]
[RequireComponent(typeof(EnemyInputCtrl))]
[RequireComponent(typeof(AIManager))]
[RequireComponent(typeof(HitAction))]
public class EnemyClassTable : CharacterClassTable
{
    [HideInInspector]
    public AICharacterModel model;
    [HideInInspector]
    public EnemyInputCtrl inputCtrl;
    [HideInInspector]
    public AIManager ai;
    [HideInInspector]
    public RayChk rayChk = new RayChk();

    protected override void Awake()
    {
        base.Awake();
        model = GetComponent<AICharacterModel>();
        
        inputCtrl = GetComponent<EnemyInputCtrl>();
        ai = GetComponent<AIManager>();
    }
}
