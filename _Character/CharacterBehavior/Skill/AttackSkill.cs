using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : SkillUnit
{


    public override void startSkill(Vector3 input_position, List<GameObject> enemyObj)
    {
        base.startSkill(input_position, enemyObj);
        skillObj.GetComponent<SkillFire>().setFire(input_position, enemyObj, skill);
        // 생성후 스크립트에 할당
        //skillObj.GetComponent<SkillFire>().setFire(input_position, enemyObj, skill);
        //Instantiate(skillObj, transform);
        //상속받아서 쓸수있게 변경
        StartCoroutine(Fire(input_position, enemyObj));
        
    }
    protected virtual IEnumerator Fire(Vector3 input_position, List<GameObject> enemyObj)
    {
        CreateObj(input_position, enemyObj);
        yield return null;
    }
    protected void CreateObj(Vector3 input_position, List<GameObject> enemyObj)
    {
        GameObject obj = Instantiate(skillObj);
        obj.transform.position = pivot.position;
        obj.GetComponent<SkillFire>().setFire(input_position, enemyObj, skill);
    }

}
