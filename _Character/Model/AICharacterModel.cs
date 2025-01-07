using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterModel : CharacterModel
{
    [SerializeField]
    private bool isAttackMob;  //true 선공/false 비선공
    private bool isBattleState; //true 전투모드// false 비전투모드(패트롤)
    private bool isHide;

    [SerializeField]
    private int alertArea;  //인식범위
    [SerializeField]
    private int patrolArea; //정찰범위

    public Dictionary<GameObject, HateLevel> hateCharacters = new Dictionary<GameObject, HateLevel>();  //적대수치를 가진 캐릭터 list

    public UnitPath pathUnit;   //path

    public bool IsAttackMob() { return isAttackMob; }

    public void setBattleState(bool isBattleState) { this.isBattleState = isBattleState; }
    public bool IsBattleState() { return isBattleState; }

    public void setHide(bool isHide) { this.isHide = isHide; }
    public bool IsHide() { return isHide; }

    public int getAlertArea() { return alertArea; }
    public int getPatrolArea() { return patrolArea; }
}
