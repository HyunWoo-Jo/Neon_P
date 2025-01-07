using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : SkillUnit
{
    public override void startSkill(Vector3 input_position, List<GameObject> enemyObj)
    {
        base.startSkill(input_position, enemyObj);
        CreateObj(input_position);
    }


    private void CreateObj(Vector3 position)
    {
        GameObject obj = Instantiate(skillObj);
        obj.transform.position = position;
    }
}
