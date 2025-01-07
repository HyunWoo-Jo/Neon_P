using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class UnitAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator instance;
    private static readonly int hashMove = Animator.StringToHash("Move");
    private static readonly int hashEnd = Animator.StringToHash("End");
    private static readonly int hashAttack = Animator.StringToHash("Attack");
    private static readonly int hashRun = Animator.StringToHash("Move(Run)");
    private static readonly int hashHit = Animator.StringToHash("Hit");
    private static readonly int hashSpecial = Animator.StringToHash("Special");

    public void Move()
    {
        instance.ResetTrigger(hashEnd);
        instance.SetTrigger(hashMove);
    }
    public void End()
    {
        instance.SetTrigger(hashEnd);
    }
    public void Attack()
    {
        instance.ResetTrigger(hashEnd);
        instance.SetTrigger(hashAttack);
    }
    public void Run()
    {
        instance.ResetTrigger(hashEnd);
        instance.SetTrigger(hashRun);
    }
    public void Hit()
    {
        instance.ResetTrigger(hashEnd);
        instance.SetTrigger(hashHit);
    }
    public void Die()
    {
        
        instance.SetTrigger("Die");
        instance.SetBool("Die true", true);
    }
    public void Special()
    {
        instance.SetTrigger(hashSpecial);
    }


    private void Awake()
    {
        instance = GetComponent<Animator>();
    }
}
