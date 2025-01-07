using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class CallBack
{
    public static SystemEvent system = new SystemEvent();
    public static TurnEvent turn = new TurnEvent();
    public static BattleEvent battle = new BattleEvent();

    public static void Clear()
    {
        system = new SystemEvent();
        turn = new TurnEvent();
        battle = new BattleEvent();

    }

}

#region __delegate__
public delegate void VoidHandeler();
public delegate bool BoolHandeler(bool isBool);
public delegate void FloatHandeler(float value);
public delegate void GameObjectHandeler(GameObject obj);
public delegate void NodeCreateHandeler(Vector3 vec, int cost);
public delegate void MoveHandeler(List<Vector2Int> list);
#endregion
#region __Event__
public class SystemEvent
{
    public event VoidHandeler endload_listener;
    public event BoolHandeler gameEnd_listener;
    public void EndLoad() { endload_listener?.Invoke(); }
    public void GameEnd(bool isEnd) { gameEnd_listener(isEnd); }
}

public class TurnEvent
{
    public event VoidHandeler start_listener;
    public event FloatHandeler end_listener;
    public void Start() { start_listener?.Invoke(); }
    public void End(float value) { end_listener?.Invoke(value); }
}

public class BattleEvent
{
    public event NodeCreateHandeler nodeCreate_listener;
    public event MoveHandeler move_listener;

    public event GameObjectHandeler attack_listener;
    public event VoidHandeler monsterDie_listener;
    public event GameObjectHandeler hit_listener;
    public event VoidHandeler attackMotionEnd_listener;
    public event VoidHandeler die_listener;
    public void DieUnit() { die_listener?.Invoke(); }
    public void NodeCreate(Vector3 pos, int cost) { nodeCreate_listener?.Invoke(pos, cost); }
    public void Move(List<Vector2Int> vec) { move_listener?.Invoke(vec); }
    public void Attack(GameObject obj) { attack_listener?.Invoke(obj); }
    public void MonsterDie() { monsterDie_listener?.Invoke(); }
    public void Hit(GameObject obj) { hit_listener?.Invoke(obj); }
    public void AttackMotionEnd() { attackMotionEnd_listener?.Invoke(); }
}
#endregion

