using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState
    {
        get { return currentState; }
        set { Transition(value); }
    }

    protected State currentState;
    protected bool isTransition;

    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();

        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    protected virtual void Transition(State st)
    {
        if (currentState == st || isTransition)

            return;

        isTransition = true;
        if (currentState != null)
            currentState.Exit();

        currentState = st;
        if (currentState != null)
            currentState.Enter();
        isTransition = false;
    }
}
