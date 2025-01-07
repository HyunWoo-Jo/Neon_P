using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayChk : IRayCover
{
    protected static readonly Vector3[] ADD_RAY_POS = new Vector3[]
    {
        new Vector3(0.5f, 0f, 0f),
        new Vector3(-0.5f, 0f, 0f),
        new Vector3(0f, 0f, 0.5f),
        new Vector3(0f, 0f, -0.5f),
    };

    public bool ObjectChk(Vector2Int position, Vector2Int targetPos)
    {
        int layer = 0b1111 << 10;
        float distance = (targetPos - position).magnitude;

        return ObjectChk(position, targetPos, distance, layer);
    }
    public bool ObjectChk(Vector2Int position, Vector2Int targetPos, float distance, int layer)
    {
        Vector3 directions = (targetPos - position).ToVector3XZ(1);

        for (int i = 0; i < 4; i++)
        {
            Vector3 rayPos = position.ToVector3XZ(1) + ADD_RAY_POS[i];
            Ray ray = new Ray(rayPos, directions);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, layer))
            {
                if (hit.collider.gameObject.GetInstanceID() != TurnManager.instacne.currentTurnUnit.GetInstanceID())
                {
                    return true;
                }
            }
        }
        return false;
    }


    public bool WallChk(Vector2Int position, Vector2Int targetPos)
    {
        int layer = LayerNumber.WALL;

        Vector3 directions = (targetPos - position).ToVector3XZ(1f);
        float distance = (targetPos - position).magnitude;


        Vector3 rayPos = position.ToVector3XZ(1);
        Ray ray = new Ray(rayPos, directions);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layer))
        {
            return true;
        }
        return false;
    }
    public bool CoverRayHitChk(Vector2Int rayPos, Vector2Int targetPos, float distance)
    {
        if (CoverObjectChk(rayPos, targetPos, distance) != null) return true;
        else return false;
    }
    public ICoverObject CoverObjectChk(Vector2Int rayPos, Vector2Int targetPos, float distance)
    {
        int layer = LayerNumber.OBJECT;

        Ray ray = new Ray(rayPos.ToVector3XZ(1f), (targetPos - rayPos).ToVector3XZ(1f));
        Debug.DrawRay(ray.origin, ray.direction * 3, Color.yellow, 3f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layer))
        {
            ICoverObject cover = hit.collider.GetComponent<ICoverObject>();
            if (cover != null) return cover;      
        }
        return null;
    }
}
