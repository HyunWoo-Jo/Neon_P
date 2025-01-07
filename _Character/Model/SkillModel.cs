using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel : MonoBehaviour
{
    [SerializeField]
    private bool isArrSkill; //true 범위스킬 / false 단일 스킬
    [SerializeField]
    private bool isAttackSkill; //true 공격스킬 / false 회복스킬
    [SerializeField]
    private int skillArr; // 스킬 범위
    [SerializeField]
    private int skillStat; // 스킬 수치
    [SerializeField]
    private int skillCount; // 스킬 사용횟수
    public int curSkillCount = 0;
    [SerializeField]
    private int skillTime; // 스킬 지속횟수
    public int curSkillTime = 0;

    public bool IsArrSkill()
    {
        return isArrSkill;
    }

    public bool IsAttackSkill()
    {
        return isAttackSkill;
    }

    public int getSkillArr()
    {
        return skillArr;
    }

    public int getSkillStat(){
        return skillStat;
    }

    public int getSkillCount()
    {
        return skillCount;
    }

    public int getSkilTime()
    {
        return skillTime;
    }

}
