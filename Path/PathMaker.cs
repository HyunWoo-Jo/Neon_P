using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{

    [HideInInspector]
    public UnitPath uP;
    public List<GameObject> path;
    [HideInInspector]
    public string fileName;

    public Color nodeColor;
    
    public Vector2Int pathPosition;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        if (path == null) return;
        Gizmos.color = nodeColor;
        GameObject node;
        for(int i=0;i< path.Count; i++)
        {
            node = path[i];
            node.transform.position = node.transform.position.ToVector2IntXZ().ToVector3XZ(node.transform.position.y);
            node.name = string.Format("path {0}", node.transform.position.ToVector2IntXZ());
            Gizmos.DrawSphere(node.transform.position,0.4f);
            Gizmos.DrawLine(node.transform.position, 
            path.Count.Equals(i + 1) ? path[0].transform.position : path[i + 1].transform.position);
            
        }
    }

    
}
