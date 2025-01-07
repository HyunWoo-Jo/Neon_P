using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrenade : SkillFire
{
    [SerializeField]
    private AudioClip _ala;
    protected override void Start()
    {
        base.Start();

    }
    protected override IEnumerator attackSkill()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.subcam.ResetParent();
        GameManager.instance.subcam.On_SubCam(this.transform, new Vector3(0,2f,-2f));
        yield return new WaitForSeconds(1.5f);
        if (enemyHitList.Count != 0)
        {
            Vector3 targetPos = this.transform.position - enemyHitList[0].transform.position;
            targetPos.y = 3f;
            GameManager.instance.subcam.On_SubCam(enemyHitList[0].transform, targetPos * 2);
        }
        else
        {
            GameManager.instance.subcam.Off_SubCam();
        }
        yield return new WaitForSeconds(0.3f);
        GetComponent<AudioSource>().PlayOneShot(_ala);
        yield return new WaitForSeconds(0.7f);
        Damage();
        GameManager.instance.subcam.Quake(0.3f, 0.3f, 0.02f);
        gameObject.GetComponent<ParticleAdapter>().Run();
        yield return new WaitForSeconds(2f);
        GameManager.instance.subcam.Off_SubCam();
        yield return new WaitForSeconds(0.5f);
        CallBack.turn.End(1f);
        Destroy(gameObject);

    }
}
