using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    [SerializeField]
    BoxCollider bat;
    PlayerClassTable table;
    private void Start()
    {
        GetComponent<AttackUnit>().rotateEnd_listener += StrikeMotion;
        table = GetComponent<PlayerClassTable>();
    }

    void StrikeMotion()
    {
        GameManager.instance.subcam.On_SubCam(transform, new Vector3(0, 4f, -3f));
        table.anim.Attack();
        table.sound.OneShot(4);
        Invoke("StrikeEnter",0.5f);
        Invoke("StrikeEnd",1.4f);
    }
    void StrikeEnter()
    {
        bat.enabled = true;
    }

    void StrikeEnd()
    {
        bat.enabled = false;
        GameManager.instance.subcam.Off_SubCam();
        CallBack.turn.End(0.5f);
        
    }

}
