using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AStarNode
{
    public Vector2Int pos;
    public Vector2Int parent;
    public int hCost;
    public int gCost;

    public AStarNode(Vector2Int pos, Vector2Int parent, int hCost, int gCost)
    {
        this.pos = pos;
        this.parent = parent;
        this.hCost = hCost;
        this.gCost = gCost;
    }
}

public class PathFinderAI : PathFinder
{
    
    private SortedDictionary<Vector2Int, AStarNode> openNodeA;
    private Dictionary<Vector2Int, AStarNode> closeNodeA;
    public List<Vector2Int> aStarPath;
    public bool isNotChk;

    private int maxGCost = 0;
    public void AStarPathFind(Vector2Int owner,  Vector2Int target)
    {
        openNodeA = new SortedDictionary<Vector2Int, AStarNode>(new Vector2IntCompareTo());
        closeNodeA = new Dictionary<Vector2Int, AStarNode>();
        aStarPath = new List<Vector2Int>();
        maxGCost = 0;
        
        AStarPathChk(owner, target, 0);
        if(aStarPath == null)
        {
            isNotChk = true;
            return;
        } else if(aStarPath.Count == 0)
        {
            isNotChk = true;
            return;
        }

        Vector2Int node;
        if(aStarPath.Count >= MIN_NODE_COUNT)
        {
            node = aStarPath[1];
        } else
        {
            isNotChk = true;
            return;
        }
        while(true)
        {
            node = openNodeA[node].parent;
            if (node == owner)
            {
                break;
            }
            aStarPath.Add(node);
        }
    }
    private void AStarPathChk(Vector2Int pos, Vector2Int targetPos, int Gcost)
    {
        for (int i = 0; i < DIRECTION_COUNT; i++)
        {
            Vector2Int chkPos = pos + DirectionVectors[i];
            if (!openNodeA.ContainsKey(chkPos)) {

                //END_CHK
                if (Stage.Instance.Grid[chkPos.x][chkPos.y].isUse) {
                    if (maxGCost < Gcost) {
                        aStarPath.Clear();
                        aStarPath.Add(chkPos);
                        aStarPath.Add(pos);
                    }
                    if (chkPos == targetPos) {
                        aStarPath.Add(chkPos);
                        aStarPath.Add(pos);
                        return;
                    }
                    continue;
                }

                Vector2Int parentPos = pos;
                int cost = Gcost;
                for (int j = 0; j < DIRECTION_COUNT; j++) {
                    Vector2Int parentChkPos = chkPos + PathFinder.DirectionVectors[j];
                    if (parentChkPos == pos) continue;
                    if (openNodeA.ContainsKey(parentChkPos)) {
                        if (cost > openNodeA[parentChkPos].gCost) {
                            cost = openNodeA[parentChkPos].gCost;
                            parentPos = parentChkPos;
                        }
                    }
                }

                AStarNode aNode = new AStarNode(chkPos, parentPos, cost + 1, pos.CompareGridPosValue(targetPos));
                openNodeA.Add(chkPos, aNode);
                closeNodeA.Add(chkPos, aNode);
            }
            
        }
        if(closeNodeA.Count != 0)
        {
            AStarNode aNode = new AStarNode(Vector2Int.zero, Vector2Int.zero, 0,0);
            int minCost = int.MaxValue;
            foreach(var item in closeNodeA)
            {
                if (minCost > item.Value.hCost)
                {    
                    aNode = item.Value;
                    minCost = aNode.hCost;
                }
            }
            if (aNode.hCost != 0)
            {
                closeNodeA.Remove(aNode.pos);   
                AStarPathChk(aNode.pos, targetPos, aNode.gCost + 1);

            }
        }
    }

    public List<Vector2Int> objList;

    public Queue<Vector2Int> PathFindObj(Vector3 startPos, int unitCost, Vector2Int target)
    {
        Queue<Vector2Int> que = new Queue<Vector2Int>();
        closeNode = new Dictionary<Vector2Int, KeyValuePair<Vector2Int,int>>();
        openNode = new PriorityQueue<Vector2Int>();
        objList = new List<Vector2Int>();

        closeNode.Add(startPos.ToVector2IntXZ(), LAST_NODE_POS);
        PathObj(startPos.ToVector2IntXZ(), 0, unitCost, que, target);
        return que;
    }

    private void PathObj(Vector2Int pos, int cost, int maxCost, Queue<Vector2Int> que, Vector2Int target)
    {
        if (cost > maxCost) return;
        que.Enqueue(pos);
        if (rayChk.ObjectChk(pos, target, 1, 1 << 11))
        {
            objList.Add(pos);
        }
        for (int i = 0; i < DIRECTION_COUNT; i++)
        {
            Vector2Int chkPos = pos + DirectionVectors[i];
            if (Stage.Instance.Grid[chkPos.x][chkPos.y].isUse) continue;
            if (!closeNode.ContainsKey(chkPos)) {
                closeNode.Add(chkPos, new KeyValuePair<Vector2Int, int>(pos, cost + Stage.Instance.Grid[chkPos.x][chkPos.y].cost));
                openNode.Enqueue(chkPos, cost + Stage.Instance.Grid[chkPos.x][chkPos.y].cost);
            }        
        }
        if (openNode.Count != 0)
        {
            Vector2Int nextPos = openNode.Dequeue();
            PathObj(nextPos, closeNode[nextPos].Value, maxCost, que, target);
        }
    }

}
