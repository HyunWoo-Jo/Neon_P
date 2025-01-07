using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Turn;
using UnityEngine.SceneManagement;
public class EndGameState : TurnStateParent
{
    public override void Enter()
    {
        CallBack.Clear();
        StartCoroutine(goTitle());
    }
    public override void Exit()
    {
        base.Exit();
    }
    IEnumerator goTitle()
    {
        yield return new WaitForSeconds(3f);
        FastLoading load = this.gameObject.AddComponent<FastLoading>();
        load.str = "Mission1End_CutScenes";
    }
}
