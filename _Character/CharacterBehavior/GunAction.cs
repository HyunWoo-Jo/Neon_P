using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObjects))]
public class GunAction : MonoBehaviour
{
    [SerializeField]
    private int gunCount;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private Transform gunPivot;
    [SerializeField]
    private GameObject muzzleFlash;

    private PoolObjects pool;
    protected void Awake()
    {
        pool = GetComponent<PoolObjects>();
    }
    public void Fire(CharacterClassTable table, GameObject target)
    {
        StartCoroutine(BulletFire(table, target));
    }
    IEnumerator BulletFire(CharacterClassTable table, GameObject target)
    {

        __CameraMng.Instance.setAttackState(this.transform);
        UI_Manager.instance.UI_playerSet(false);
        yield return new WaitForSeconds(0.5f);
        table.anim.Attack();
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < gunCount; i++)
        {
            yield return new WaitForSeconds(attackDelay - 0.05f);
            StartCoroutine(Flash());
            table.sound.OneShot(0);
            yield return new WaitForSeconds(0.05f);
            GameObject obj = pool.BorrowObject();

            obj.transform.position = gunPivot.position;
            obj.transform.rotation = gunPivot.transform.rotation;
            obj.GetComponent<Bullet>().Enter(target, pool);
        }
        table.anim.End();
        yield return new WaitForSeconds(0.9f);
        CallBack.battle.AttackMotionEnd();
    }


    IEnumerator Flash()
    {
        GameObject obj = Instantiate(muzzleFlash);
        obj.transform.SetParent(gunPivot);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}
