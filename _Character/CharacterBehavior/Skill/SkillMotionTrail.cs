using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMotionTrail : SkillUnit
{
    PlayerClassTable table;
    MotionTrail trail;
    public int count = 3;
    private int skilTurnCount = 0;
    public GameObject _light;
    protected override void Start()
    {
        base.Start();
        table = GetComponent<PlayerClassTable>();
        trail = skillObj.GetComponent<MotionTrail>();
        trail.transform.SetParent(GameManager.instance.transform);
        
    }
    public override void startSkill(Vector3 input_position, List<GameObject> enemyObj)
    {
        base.startSkill(input_position, enemyObj);
        StartCoroutine(SetAction());
    }
    IEnumerator SetAction()
    {
        GameManager.instance.subcam.On_SubCam(this.transform, new Vector3(0, 4f, 2f));
        yield return new WaitForSeconds(1f);
        _light.SetActive(true);
        table.sound.OneShot(3);
        trail.enabled = true;
        skilTurnCount = 0;
        CallBack.turn.start_listener += TurnChk;
        table.anim.Special();
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.subcam.Quake(1.5f, 1f, 0.01f);
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.subcam.Off_SubCam();
        UI_Manager.instance.UI_playerSet(true);
    }
    
    private void TurnChk()
    {
        if (!this.gameObject.GetInstanceID().Equals(TurnManager.instacne.currentTurnUnit.GetInstanceID())) return;
        skilTurnCount++;
        if(count <= skilTurnCount)
        {
            trail.enabled = false;
            CallBack.turn.start_listener -= TurnChk;
        }
    }
}
