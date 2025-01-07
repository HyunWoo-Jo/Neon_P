using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFX.SFX;
public class RocketSkill : SkillUnit
{
    [SerializeField]
    Transform dronCam;
    [SerializeField]
    DronMove dron;
    [SerializeField]
    int damage = 20;
    [SerializeField]
    GameObject muzzle;
    public override void startSkill(Vector3 input_position, List<GameObject> enemyObj)
    {
        base.startSkill(input_position, enemyObj);
        if (enemyObj.Count == 0) return;
        StartCoroutine(Fire(enemyObj));
    }
    IEnumerator Fire(List<GameObject> enemyObj)
    {
        GameManager.instance.mask.IsRun = false;
        __CameraMng.Instance.setAttackState(dronCam);
        UI_Manager.instance.UI_playerSet(false);
        dron.Attack();
        enemyObj[0].GetComponent<EnemyClassTable>().attack.HitBuffer(damage,true);
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 30; i++)
        {
            GameObject m = Instantiate(muzzle);
            m.transform.position = pivot.position;
            m.transform.rotation = pivot.rotation;

            GameObject obj = Instantiate(skillObj);
            obj.transform.position = pivot.position;
            obj.transform.rotation = pivot.rotation;

            TargetSet(obj.GetComponent<SFX_PhysicsHomingMissile>(), enemyObj[Random.Range(0, enemyObj.Count)].transform);
            
            yield return new WaitForSeconds(0.05f);
        }
        dron.Standard();
        __CameraMng.Instance.setBattleState(transform);
        CallBack.turn.End(1.5f);
    }
    private void TargetSet(SFX_PhysicsHomingMissile missile,Transform tr)
    {
        missile.TargetTransform = tr;
        missile.LaunchPosition = tr.position;
    }

}
