using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeInput : MonoBehaviour, IRayCastHit {
    GameObject lineObj;
    public List<Vector2Int> path = new List<Vector2Int>();
    public Vector2Int pos;
    
    private void OnEnable()
    {
        path = null;
        pos = transform.position.ToVector2IntXZ();
    }
    private void OnDisable()
    {
        if (lineObj != null) Destroy(lineObj);
    }


    public void RayEnter()
    {
        if (!NodeCreate.instance.isUse) return;
        path = NodeCreate.instance.path.PathOptimizationList(pos);
        lineObj = NodeCreate.instance.LineCreate(path, transform.position.y);
    }

    public void RayExit()
    {
        if (!NodeCreate.instance.isUse) return;
        Destroy(lineObj);
    }

    public void RayDown()
    {
        if (path == null) return;
        if (!NodeCreate.instance.isUse) return;
        NodeCreate.instance.isUse = false;
        NodeCreate.instance.NodeDelete();
        Stage.Instance.Grid[path[0].x][path[0].y].isUse = true;
        Stage.Instance.Grid[path[path.Count - 1].x][path[path.Count - 1].y].isUse = false;
        CallBack.battle.Move(path);
        Destroy(lineObj);
    }
}
