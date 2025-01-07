using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterModel))]
public class PlayerClassTable : CharacterClassTable
{
    [HideInInspector]
    public CharacterModel model;
    [HideInInspector]
    public SkillUnit skil;

    protected override void Awake()
    {
        base.Awake();

        model = GetComponent<CharacterModel>();
        skil = GetComponent<SkillUnit>();
    }


}
