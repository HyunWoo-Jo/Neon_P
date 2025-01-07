using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverObject : MonoBehaviour, ICoverObject
{
    [SerializeField]
    private float hight = 1f;
    [SerializeField]
    private Vector2Int[] coverPoint;
    public Vector2Int[] GetCoverPoint()
    {
        return coverPoint;
    }

    public float GetHight()
    {
        return hight;
    }

    public Vector2Int Position()
    {
        return transform.position.ToVector2IntXZ();
    }
//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.yellow;
//        for(int i = 0; i < coverPoint.Length; i++)
//        {
//            Vector3 coverVec = coverPoint[i].ToVector3XZ(1.5f);
//            Gizmos.DrawSphere(coverVec, 0.3f);
//            Gizmos.DrawLine(coverVec, transform.position);
//        }
//    }
//#endif
}
