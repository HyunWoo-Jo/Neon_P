using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinder
{
    public RayChk rayChk = new RayChk();

    public static readonly Vector2Int[] DirectionVectors =
    {
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0)
    };
    protected static readonly KeyValuePair<Vector2Int,int> LAST_NODE_POS = new KeyValuePair<Vector2Int, int>(new Vector2Int(1234567, 1234567), -1);
    protected static readonly int MIN_NODE_COUNT = 2;
    public static readonly int DIRECTION_COUNT = 4;
    public Dictionary<Vector2Int, KeyValuePair<Vector2Int, int>> closeNode;
    protected PriorityQueue<Vector2Int> openNode;
    public List<List<bool>> findedGrid = new List<List<bool>>();
    public HashSet<Vector2Int> moveAbleNode_hashSet = new HashSet<Vector2Int>();
    int maxCost;
    private Vector2Int startPos;
    private float unitYPos = 1f;
    private float thickness = 0.3f;

    // 유닛의 이동 코스트 범위 내에 이동 가능 그리드 검색
    public void MoveAbleGridFind(Vector3 sPos, int unitCost) {
        moveAbleNode_hashSet.Clear();
        InitFindedGrid();
        startPos = sPos.ToVector2IntXZ();
        maxCost = unitCost;
        Queue<KeyValuePair<Vector2Int, int>> serchQue = new Queue<KeyValuePair<Vector2Int, int>>();
        // 이동 가능 그리드 bfs 검색
        SerchMoveAbleDireciton(startPos, serchQue, 0);
        while(serchQue.Count > 0) {
            KeyValuePair<Vector2Int, int> serchData = serchQue.Dequeue();
            SerchMoveAbleDireciton(serchData.Key, serchQue, serchData.Value);
        }
    }
    // 이동 가능한 노드 검색
    private void SerchMoveAbleDireciton(Vector2Int pos, Queue<KeyValuePair<Vector2Int, int>> serchQue, int cost)
    {
        if (cost > maxCost) return;
        findedGrid[pos.x][pos.y] = true;
        moveAbleNode_hashSet.Add(pos);
        for (int i = 0; i < 4; i++) {
            Vector2Int serchPos = pos + DirectionVectors[i];
            if (serchPos.x < 0 || serchPos.y < 0 || serchPos.x >= Stage.Instance.MaxGridSize.x || serchPos.y >= Stage.Instance.MaxGridSize.y) continue;
            if (findedGrid[serchPos.x][serchPos.y]) continue;
            var gridData = Stage.Instance.Grid[serchPos.x][serchPos.y];
            if (gridData.isUse) continue;
            gridData.prePos = pos;
            serchQue.Enqueue(new KeyValuePair<Vector2Int, int>(serchPos, cost + gridData.cost));
        }
    }

    private void InitFindedGrid() {
        findedGrid.Clear();
        for(int x =0;x< Stage.Instance.MaxGridSize.x; x++) {
            findedGrid.Add(new List<bool>(Stage.Instance.MaxGridSize.y));
            for(int y= 0; y < Stage.Instance.MaxGridSize.y; y++) {
                findedGrid[x].Add(false);
            }
        }
    }

    // 찾은 경로를 List에 담아 반환
    public List<Vector2Int> GetPath(Vector2Int pos) {
        List<Vector2Int> path_list = new List<Vector2Int>();
        Vector2Int prePos = pos;
        path_list.Add(prePos);
        while (prePos != startPos) {
            prePos = Stage.Instance.Grid[prePos.x][prePos.y].prePos;
            path_list.Add(prePos);
        }
        return path_list;
    }
    // Ray를 이용해 경로 최적화
    public List<Vector2Int> PathOptimizationList(Vector2Int pos) {
        List<Vector2Int> optimizationPath_list = new List<Vector2Int>();
        List<Vector2Int> path_list = GetPath(pos);
        optimizationPath_list.Add(path_list[0]);
        int leftIndex = 0, rightIndex = 1;
        int layer = LayerNumber.WALL | LayerNumber.OBJECT;
        while (true) {
            Vector2Int vecDistance = path_list[rightIndex] - path_list[leftIndex];
            Vector3 direction = new Vector3(vecDistance.x, 0, vecDistance.y).normalized;
            //좌우에 대응하는 백터와 센터 추가
            Vector3 left = new Vector3(-direction.z, 0, direction.x) * thickness;
            Vector3 right = new Vector3(direction.z, 0, -direction.x) * thickness;
            Vector3 center = path_list[leftIndex].ToVector3XZ(unitYPos);
            // 3개의 Ray 생성
            Ray centerRay = new Ray(center, direction);
            Ray leftRay = new Ray(center + left, direction);
            Ray rightRay = new Ray(center + right, direction);
            float distance = vecDistance.magnitude;
            // ray 검사
            if (Physics.Raycast(centerRay, distance, layer) ||
                Physics.Raycast(leftRay, distance, layer) ||
                Physics.Raycast(rightRay, distance, layer)) {
                if (rightIndex > leftIndex + 1) {
                    optimizationPath_list.Add(path_list[rightIndex - 1]);
                    leftIndex = rightIndex - 1;
                    rightIndex = leftIndex + 1;
                } else {
                    leftIndex++;
                    rightIndex = leftIndex + 1;
                }
            } else {
                rightIndex++;
                if (rightIndex >= path_list.Count) {
                    optimizationPath_list.Add(path_list[rightIndex - 1]);
                    break;
                }
            }
        }
        return optimizationPath_list;
    }
}
