using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : MonoBehaviour
{

    protected SkillModel skill;
    protected Vector3 input_position;
    protected List<AttackUnit> enemyHitList = new List<AttackUnit>();
    protected List<HitAction> enemyActList = new List<HitAction>();
    protected List<bool> isEnemy = new List<bool>();

    //[SerializeField]


    public void setFire(Vector3 input_position , List<GameObject> enemyObjList, SkillModel skill)
    {
        this.input_position = input_position;
        this.skill = skill;
        //List 비워주는 클리어 함수 추가
        ListClear();
        for (int i = 0; i<enemyObjList.Count; i++)
        {
            //List 할당 방식이 잘못되 변경함
            enemyHitList.Add(enemyObjList[i].GetComponent<AttackUnit>());
            enemyActList.Add(enemyObjList[i].GetComponent<HitAction>());
            isEnemy.Add(enemyObjList[i].GetComponent<CharacterModel>().IsEnemy());

            //enemyHitList[i] = enemyObjList[i].GetComponent<AttackUnit>();
            //enemyActList[i] = enemyObjList[i].GetComponent<HitAction>();
            //isEnemy[i] = enemyObjList[i].GetComponent<CharacterModel>().IsEnemy();
        }
    }
    void ListClear()
    {
        enemyHitList.Clear();
        enemyActList.Clear();
        isEnemy.Clear();
    }
    protected virtual void Start()
    {
        StartCoroutine(attackSkill());
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, input_position, Time.deltaTime * 15.0f);
    }
    
  
    protected virtual IEnumerator attackSkill()
    {
        //카메라 결속
        yield return new WaitForSeconds(3f);
        Damage();
        gameObject.GetComponent<ParticleAdapter>().Run();
        CallBack.turn.End(1f);
        Destroy(gameObject);
        
    }
    protected void Damage()
    {
        for (int i = 0; i < enemyHitList.Count; i++)
        {
            if (isEnemy[i])
            {
                enemyHitList[i].HitBuffer(skill.getSkillStat(), true);
                enemyActList[i].Hit(transform);
                enemyHitList[i].RealHit();

            }
        }
    }

}
