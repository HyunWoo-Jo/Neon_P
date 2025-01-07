using System.Collections;
using UnityEngine;
public class AttackUnit : MonoBehaviour
{
    private CharacterModel model;
    public VoidHandeler rotateEnd_listener;
    public static DetourUnit detout;

    public float delay;
    public float attackDelay;
    public int attackCount = 1;
    bool isHit = false;
    private void Awake()
    {
        if (detout == null) detout = new DetourUnit();
    }
    void Start()
    {
        model = gameObject.GetComponent<CharacterModel>();
        model.reloadBullet();
        model.setNewCurHp();
        CallBack.turn.start_listener += delegate { isHit = false; };
    }

    public void AttackTarget(GameObject target)
    {
        UI_Manager.instance.UI_playerSet(false);
        if (!HaveBulletChk())
        {
            Reload();
            return;
        }
        UseBullet();
        int distance2Target = this.transform.position.CompareGridPosValue(target.transform.position);
        int damage = model.getDam() + distance2Target.AccuracyRate().AccMDamage();
        AttackUnit targetA = target.GetComponent<AttackUnit>();
        targetA.HitBuffer(damage, true);
        
        MoveUnit moveUnit = GetComponent<MoveUnit>();
        if (detout.IF_ExistObjectDetOur(transform.position.ToVector2IntXZ(),
            target.transform.position.ToVector2IntXZ(), moveUnit))
        {
            this.target = target;
            moveUnit.moveEnd_listener += LookTarget;
            __CameraMng.Instance.setAttackState(transform);
        }
        else
        {
            StartCoroutine(ILookTarget(target));
        }


        HaveBulletChk();
    }
    GameObject target;
    void LookTarget()
    {
        StartCoroutine(ILookTarget(target));
        GetComponent<MoveUnit>().moveEnd_listener -= LookTarget;
    }
    
    private bool HaveBulletChk()
    {
        if (model.IsHaveGun() && model.getCurBullet() == 0)
        {
            model.setIsBulletEmpty(true);
            return false;
        }
        model.setIsBulletEmpty(false);
        return true;                
    }
    private void UseBullet()
    {
        model.setCurBullet(1);
    }

    private bool AccuRateCompute(int accur)
    {
        int range = Random.Range(1,101);
        if (accur >= range) return true;
        else return false;
        
    }

    public void HitBuffer(int damage, bool hit)
    {
        TurnManager.instacne.damageBuffer = new Damage(damage,hit);   
    }

    private void Reload() {
        model.reloadBullet();
        CallBack.turn.End(1f);
    }

    public void RealHit()
    {
        if (!isHit)
        {
            int damage = TurnManager.instacne.damageBuffer.bufferDamage - model.getShield();
            UI_Manager.instance.damageCreate.CreateUIBar(transform.position, damage);
            model.setCurHp(model.getCurHp() - damage);
            isHit = true;
        }
    }
  

    IEnumerator ILookTarget(GameObject enemy)
    {
   
        Vector3 targetVec = enemy.transform.position;
        Vector3 dir = targetVec - transform.position;
        int i = 0;
        for (i = 0; i < 40; i++)
        {
            targetVec = enemy.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), model.getRotSpeed() * Time.deltaTime);
            yield return null;
        }        
        rotateEnd_listener?.Invoke();
        if (!model.IsHaveGun())
        {
            yield return new WaitForSeconds(delay);
            for (i = 0; i < attackCount; i++)
            {
                enemy.GetComponent<HitAction>().Hit(this.gameObject.transform);
                yield return new WaitForSeconds(attackDelay);
            }
        }
        if (delay != 0)
            CallBack.battle.AttackMotionEnd();           
    }
}
