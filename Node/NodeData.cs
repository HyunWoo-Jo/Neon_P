using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NodeData
{
    public Vector2Int pos;
    public NodeType nodeType;
    public UnitType unitType = UnitType.Empty;
    public Vector2Int prePos;
    public int cost;
    public int H;
    public int G;
    public int F;

    public int defendPlus;
    public int coverPlus;
    public bool isUse = false;
    public NodeData()
    {
       
    }

    public NodeData(Vector2Int pos, NodeType nodeType, int cost, int defendPlus, int coverPlus)
    {
        this.pos = pos;
        this.nodeType = nodeType;
        this.cost = cost;
        this.defendPlus = defendPlus;
        this.coverPlus = coverPlus;
    }
}
