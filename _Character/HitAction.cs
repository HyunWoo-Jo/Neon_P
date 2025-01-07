using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turn.AI;

public class HitAction : MonoBehaviour
{
    private CharacterClassTable table;
    private CharacterModel model;
    private AICharacterModel enemyModel;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private float hitBackMovePower;
    [SerializeField]
    private float recoverSpeed;

    private Vector3 startPos;
    private bool isStartChk;

    private Rigidbody rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        table = GetComponent<CharacterClassTable>();
        enemyModel = GetComponent<AICharacterModel>();
        model = GetComponent<CharacterModel>();
    }
    public void Hit(Transform other)
    {
        if (!TurnManager.instacne.damageBuffer.bufferIsHit) return;
        if (!isStartChk) StartCoroutine(RecoverTime());
        if (enemyModel != null && !isHit)
        {
            StartCoroutine(HateAdd());
        }
        GameManager.instance.mask.IsRun = false;
        table.anim.Hit();
        table.sound.OneShot(1);

        Vector3 pos = other.position - this.transform.position;
        Quaternion rotatePos = Quaternion.LookRotation(pos);
        Vector3 rot = pos;
        rot.y = 0;


        this.transform.rotation = Quaternion.LookRotation(rot);
        Vector3 backPos = pos * -1f;
        pos = pos.normalized;
        backPos.y = 0f;
        transform.position = transform.position + (backPos * hitBackMovePower);
        StartCoroutine(HitRim());
        CallBack.battle.Hit(this.gameObject);
        table.attack.RealHit();

        if (particle != null)
        {
            GameObject obj = Instantiate(particle);
            obj.transform.rotation = rotatePos;
            obj.transform.position = this.transform.position + (pos * 0.5f);
            StartCoroutine(Delete(obj));
        }
        if (model.getCurHp() <= 0)
        {

            if (!isDie)
            {
                
                if (model.IsEnemy())
                    StartCoroutine(MonsterDie());
                else
                    PlayerDie();

            }
        }
        
    }
    bool isDie = false;
    void PlayerDie()
    {
        isDie = true;
        table.anim.Die();
        for (int i = 0; i < TurnManager.instacne.sequenceList.Count; i++)
        {
            if (TurnManager.instacne.sequenceList[i].GetInstanceID() == this.gameObject.GetInstanceID())
            {
                TurnManager.instacne.sequenceList.RemoveAt(i);
                break;
            }
        }
        Stage.Instance.unitData.playerUnitDic.Remove(this.gameObject);
        Stage.Instance.unitData.unitDic.Remove(this.gameObject);
        GetComponent<CharacterModel>().setIsDead(true);
        UI_Manager.instance.timeline.DeleteUnit(gameObject.GetInstanceID());
        CallBack.battle.DieUnit();
    }
    IEnumerator MonsterDie()
    {
        isDie = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<AIManager>().ChangeState<DieState>();
    }
    IEnumerator Delete(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        Destroy(obj);
    }

    IEnumerator RecoverTime()
    {
        startPos = transform.position;
        isStartChk = true;       
        for(int i = 0; i < 30; i++)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, recoverSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = startPos;
        isStartChk = false;
    }
    IEnumerator HitRim()
    {
        table.rim.IsChange = true;
        yield return new WaitForSeconds(0.03f);
        table.rim.IsChange = false;
    }
    bool isHit = false;
    IEnumerator HateAdd()
    {
       
        HateLevel hate;
        isHit = true;
        if (enemyModel.hateCharacters.ContainsKey(TurnManager.instacne.currentTurnUnit))
        {
            hate = enemyModel.hateCharacters[TurnManager.instacne.currentTurnUnit];
            hate.setHateLevel(hate.getHateLevel() + 20);
            enemyModel.hateCharacters[TurnManager.instacne.currentTurnUnit] = hate;
        }
        else
        {     
            hate = new HateLevel();
            hate.setHateLevel(20);
            enemyModel.hateCharacters.Add(TurnManager.instacne.currentTurnUnit, hate);
        }
    
        yield return new WaitForSeconds(4f);
        isHit = false;
    }
}
