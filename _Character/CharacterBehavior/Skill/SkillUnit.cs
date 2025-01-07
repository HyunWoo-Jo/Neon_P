using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnit : MonoBehaviour
{
    
    protected SkillModel skill;
    protected List<GameObject> enemyObjList;
 
    //serialize 화
    [SerializeField]
    protected GameObject skillObj;
    //pivot 추가
    [SerializeField]
    protected Transform pivot;

    protected virtual void Start()
    {
        skill = gameObject.GetComponent<SkillModel>();      
    }

    public virtual void startSkill(Vector3 input_position, List<GameObject> enemyObj)
    {
        this.enemyObjList = enemyObj;
        skill.curSkillCount--;
        skill.curSkillTime++;
    }

    
}
