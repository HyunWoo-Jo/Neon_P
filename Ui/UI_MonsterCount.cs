using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_MonsterCount : MonoBehaviour
{
    [SerializeField]
    private Text targetText;
    [SerializeField]
    private int targetCount;
    int count = 0;

    private void Start()
    {
        CallBack.battle.monsterDie_listener += CountIncrement;
        Renew();
    }

    void CountIncrement()
    {
        count++;
        Renew();
    }

    void Renew()
    {
        targetText.text = string.Format("{0} / {1}", count, targetCount);
    }
}
