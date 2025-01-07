using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveUnit))]
[RequireComponent(typeof(AttackUnit))]
[RequireComponent(typeof(UnitAnimation))]
[RequireComponent(typeof(UnitSound))]
public abstract class CharacterClassTable : MonoBehaviour
{

    [HideInInspector]
    public MoveUnit move;
    [HideInInspector]
    public AttackUnit attack;
    [HideInInspector]
    public UnitAnimation anim;
    [HideInInspector]
    public UnitSound sound;
    [HideInInspector]
    public RimShader rim;
    [HideInInspector]
    public UI_CharaterBar ui_bar;
    protected virtual void Awake()
    {
        move = GetComponent<MoveUnit>();
        attack = GetComponent<AttackUnit>();
        anim = GetComponent<UnitAnimation>();
        sound = GetComponent<UnitSound>();
        rim = GetComponent<RimShader>();
        ui_bar = GetComponentInChildren<UI_CharaterBar>();
        if (rim == null)
            rim = GetComponentInChildren<RimShader>();

    }
    private void Start()
    {
        Vector2Int pos = transform.position.ToVector2IntXZ();
        Stage.Instance.Grid[pos.x][pos.y].isUse = true;
    }
}
