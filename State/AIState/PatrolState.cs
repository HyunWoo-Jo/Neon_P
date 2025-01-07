using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Turn.AI
{
    public class PatrolState : PeaceState
    {
        Vector2Int startPos;
        public override void Enter()
        {
            base.Enter();
            startPos = transform.position.ToVector2IntXZ();
            Stage.Instance.Grid[startPos.x][startPos.y].isUse = false;

            owner.table.anim.Move();
            StartCoroutine(PatrolMove(owner.table.model.pathUnit));
        }
        public override void Exit()
        {
            base.Exit();
            owner.table.anim.End();
            StopAllCoroutines();
        }



        IEnumerator PatrolMove(UnitPath path)
        {
            int count = 0;
            Vector3 targetVec = path.data[0].ToVector3XZ(transform.position.y);
            Vector3 dir = targetVec - transform.position;
            while (true)
            {
                if (targetVec == transform.position)
                {
                    count++;
                    if (path.data.Count <= count)
                    {
                        count = 0;
                    }
                    targetVec = path.data[count].ToVector3XZ(transform.position.y);
                    dir = targetVec - transform.position;
                    owner.table.anim.End();
                    yield return new WaitForSeconds(Random.Range(2f, 4f));
                    owner.table.anim.Move();
                }
                transform.position = Vector3.MoveTowards(transform.position, targetVec, (owner.table.model.getMoveSpeed() * Time.deltaTime * 0.3f));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), owner.table.model.getRotSpeed() * Time.deltaTime);
                yield return null;
            }
        }
        IEnumerator PatrolRandomMove(int range)
        {
            Vector3 targetVec = transform.position;
            Vector3 dir = targetVec - transform.position;

            while (true)
            {
                while (true)
                {


                    yield return null;
                }
            }

        }



    }
}