using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunAction))]
public class PlayerGunUnit : StandardPlayerUnit
{
    private GunAction gun;
    protected override void Awake()
    {
        base.Awake();
        gun = GetComponent<GunAction>();
    }

    protected override void TurnEnter()
    {
        base.TurnEnter();
        if (isEnd) return;
        table.attack.rotateEnd_listener += Fire;
        CallBack.battle.attackMotionEnd_listener += End;

    }

    protected override void TurnExit()
    {
        base.TurnExit();
        if (!isEnd) return;
        table.attack.rotateEnd_listener -= Fire;
        CallBack.battle.attackMotionEnd_listener -= End;
    }
    void End()
    {
        CallBack.turn.End(1f);
    }

    void Fire()
    {
        gun.Fire(table, target);
    }
}
