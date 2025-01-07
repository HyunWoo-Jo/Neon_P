using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetourUnit
{
    private static IRayCover ray = new RayChk();
    private static float RAY_DISTANCE = 3f;
    List<Vector2Int> movePossibleList;

    public bool IF_ExistObjectDetOur(Vector2Int startPos,Vector2Int targetPos, MoveUnit move)
    {
        ICoverObject cover = ray.CoverObjectChk(startPos, targetPos, RAY_DISTANCE);
        if (cover == null) return false;
        //close target
        if (Vector2Int.Distance(cover.Position(), startPos) > Vector2Int.Distance(targetPos, startPos))
            return false;
        MovePossiblePos(startPos, cover.GetCoverPoint());
        if (movePossibleList.Count == 0) return false;

        TargetSeenPos(targetPos);

        Vector2Int closePos = ClosePos(startPos);
        if (closePos.Equals(Vector2Int.zero)) return false;

        move.SingleMove(closePos);
        return true;
    }

    private void MovePossiblePos(Vector2Int startPos, Vector2Int[] movePos) {
        movePossibleList = new List<Vector2Int>();
        for (int i = 0; i < movePos.Length; i++) {
            float distance = Vector2Int.Distance(startPos, movePos[i]);
            if (!ray.CoverRayHitChk(startPos, movePos[i], distance)) {
                if (!Stage.Instance.Grid[movePos[i].x][movePos[i].y].isUse)
                    movePossibleList.Add(movePos[i]);
            }
        }
    }
    private void TargetSeenPos(Vector2Int targetPos)
    {
        List<Vector2Int> seenPoslist = new List<Vector2Int>();
        for(int i = 0; i < movePossibleList.Count; i++)
        {
            float distance = Vector2Int.Distance(movePossibleList[i], targetPos);
            if (!ray.CoverRayHitChk(movePossibleList[i], targetPos, distance))
            {
                seenPoslist.Add(movePossibleList[i]);
            }
        }
        if (seenPoslist.Count != 0)
            movePossibleList = seenPoslist;   
    }
    private Vector2Int ClosePos(Vector2Int startPos)
    {
        Vector2Int closePos = Vector2Int.zero;
        float closeDistance = float.MaxValue;
        for(int i = 0; i < movePossibleList.Count; i++)
        {
            float distance = Vector2Int.Distance(movePossibleList[i], startPos);
            if(distance < closeDistance)
            {
                closePos = movePossibleList[i];
                closeDistance = distance;
            }
        }
        return closePos;
    }
}
