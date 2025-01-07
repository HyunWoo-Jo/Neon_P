using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnitTurn : MonoBehaviour
{
    private VoidHandeler Enter;
    private VoidHandeler Exit;

    protected IUnitTurn()
    {
        Enter += TurnEnter;
        Exit += TurnExit;
    }

    protected abstract void TurnEnter();
    protected abstract void TurnExit();

    public void TurnStart()
    {
        Enter?.Invoke();
    }
    public void TurnEnd()
    {
        Exit?.Invoke();
    }
}
