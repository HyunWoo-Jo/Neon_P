using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineCreater : MonoBehaviour
{
    private PoolObjects linePool;
    public PathFinder path = new PathFinder();
    private List<List<Vector3>> list;
    [SerializeField]
    private float hight;

    private Queue<LineRenderer> linesQue = new Queue<LineRenderer>();
    private void Awake()
    {
        linePool = GetComponent<PoolObjects>();
    }

   public void LineCreate(Vector3 starPos, int cost)
    {
        LineVectorCreate();
        SetLine();
    }
    public void DeleteLine()
    {
        while(linesQue.Count != 0)
        {
            linePool.PayBackObject(linesQue.Dequeue().gameObject);
        }
    }
    private void SetLine()
    {
        for(int i = 0; i < list.Count; i++)
        {
            LineRenderer line = linePool.BorrowObject().GetComponent<LineRenderer>();
            line.positionCount = list[i].Count;
            for (int j = 0; j < list[j].Count; j++)
            {
                line.SetPosition(j, list[i][j]);
            }
            linesQue.Enqueue(line);
            line.gameObject.SetActive(true);
        }
    }

    
    private void LineVectorCreate()
    {
        list = new List<List<Vector3>>();
        foreach(var item in path.closeNode)
        {
            for (int i = 0; i < PathFinder.DIRECTION_COUNT; i++)
            {
                Vector2Int pos = item.Key + PathFinder.DirectionVectors[i];
                if (!path.closeNode.ContainsKey(pos))
                {
                    List<Vector3> listVec = GetLine(item.Key, PathFinder.DirectionVectors[i]);
                    list.Add(listVec);
                }
            }
        }
    }
    private List<Vector3> GetLine(Vector2Int mainPos,Vector2Int addPos) { 
        Vector2[] pos = LinePos(mainPos,addPos);
        List<Vector3> listVec = new List<Vector3>();
        for (int i = 0; i < pos.Length; i++)
        {
            listVec.Add(new Vector3(pos[i].x, hight, pos[i].y));
        }
        return listVec;
 
    }

    private Vector2[] LinePos(Vector2Int mainPos, Vector2Int addPos)
    {
        Vector2[] pos = new Vector2[2];
        pos[0] = mainPos + (Vector2)addPos * 0.5f;
        pos[1] = pos[0];
        if (addPos.y == 0)
        {
            pos[0].y -= 0.5f;
            pos[1].y += 0.5f;
        }
        else if (addPos.x == 0)
        {
            pos[0].x -= 0.5f;
            pos[1].x += 0.5f;
        }
        return pos;
    }
}
