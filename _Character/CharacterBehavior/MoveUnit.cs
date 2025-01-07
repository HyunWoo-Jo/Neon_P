using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    private CharacterModel p_model;

    private CharacterClassTable table;
    public VoidHandeler moveStart_listener;
    public VoidHandeler moveEnd_listener;
    public bool isSigleMove = false;
    public Vector3 beforePos;

    private void Awake()
    {
        p_model = gameObject.GetComponent<CharacterModel>();
        table = GetComponent<CharacterClassTable>();
    }
    private void Start()
    {
        
    }
    public void SingleMove(Vector2Int moveNode)
    {
        CallBack.battle.attackMotionEnd_listener += MoveBeforPos;
        beforePos = transform.position;
        isSigleMove = true;  
        List<Vector2Int> list = new List<Vector2Int>();
        list.Add(moveNode);    
        StartCoroutine(IMove(list));
    }

    void MoveBeforPos()
    {
        List<Vector2Int> list = new List<Vector2Int>();
        list.Add(beforePos.ToVector2IntXZ());
        StartCoroutine(IMove(list));
        CallBack.battle.attackMotionEnd_listener -= MoveBeforPos;
        __CameraMng.Instance.setBattleState(this.transform);
        CallBack.turn.start_listener += SingleReset;
    }
    
    public void Move(List<Vector2Int> moveNode)
    {
        beforePos = transform.position;
        __CameraMng.Instance.setAttackState(this.transform);
        moveEnd_listener += MoveEndCamera;
        StartCoroutine(IMove(moveNode));
    }
    void SingleReset()
    {
        isSigleMove = false;
        CallBack.turn.start_listener -= SingleReset;
    }
    void MoveEndCamera()
    {
        __CameraMng.Instance.setBattleState(this.transform);
        moveEnd_listener -= MoveEndCamera;
    }

    IEnumerator IMove(List<Vector2Int> moveNode)
    {
        moveStart_listener?.Invoke();
        bool isRun = false;
        bool isWalk = false;
        var pos = transform.position.ToVector2IntXZ();
        Stage.Instance.Grid[pos.x][pos.y].isUse = false;
        for (int i = moveNode.Count - 1; i >= 0; i--)
        {       
            Vector3 targetVec = moveNode[i].ToVector3XZ(transform.position.y);
            Vector3 dir = targetVec - transform.position;
            float speed = p_model.getMoveSpeed();
            
            if (dir.magnitude > 3)
            {
                if (!isRun)
                {
                    table.anim.Run();
                    isWalk = false;
                    isRun = true;
                }
                speed *= 1.5f;
            }
            else
            {
                if (!isWalk)
                {
                    table.anim.Move();
                    isWalk = true;
                    isRun = false;
                }
            }

            while (!transform.position.Equals(targetVec))
            {
                transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), p_model.getRotSpeed() * Time.deltaTime);
                //Vector3 rot = transform.InverseTransformPoint(targetVec);
                //float trot = Mathf.Atan2(rot.x, rot.z) * Mathf.Rad2Deg;
                //transform.Rotate(Vector3.up * trot * RotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
        var pos1 = transform.position.ToVector2IntXZ();
        Stage.Instance.Grid[pos1.x][pos1.y].isUse = true;
        table.anim.End();

        moveEnd_listener?.Invoke();
    }
}
