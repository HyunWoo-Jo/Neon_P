using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRayCover
{
    bool CoverRayHitChk(Vector2Int rayPos, Vector2Int targetPos, float distance);
    ICoverObject CoverObjectChk(Vector2Int rayPos, Vector2Int targetPos, float distance);
}
