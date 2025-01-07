using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAttackSkill : AttackSkill
{
    [SerializeField]
    private GameObject muzle;
    protected override IEnumerator Fire( Vector3 input_position, List<GameObject> enemyObj)
    {
        CharacterClassTable table = GetComponent<CharacterClassTable>();
        GameManager.instance.subcam.SetParent(this.transform);
        GameManager.instance.subcam.On_SubCam(this.transform, new Vector3(-1, 3, 3));
        transform.rotation = Quaternion.LookRotation(input_position - transform.position);
        yield return new WaitForSeconds(0.7f);
        table.anim.Attack();
        yield return new WaitForSeconds(0.4f);
        GameObject mz = Instantiate(muzle, pivot);
        mz.transform.localPosition = Vector3.zero;
        CreateObj(input_position, enemyObj);
        table.anim.End();
    }
}
