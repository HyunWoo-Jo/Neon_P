using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExpansionData
{
    public static int AngleRotate(this float y, float targetY)
    {
        float pY = y;
        float tY = targetY;

        if (pY / 180f >= 1f)
            pY = -(180 - (pY % 180));
        if (tY / 180f >= 1f)
            tY = -(180 - (tY % 180));
        float a = tY - pY;
        return (int)a;
    }

    public static int AccuracyRate(this int distance)
    {
        int A = 100 - (distance * 5 - 5);
        if (A < 0) A = 0;
        return A;
    }
    public static int AccMDamage(this int accuracy)
    {
        int a = 4 - (int)Mathf.Round((100 - accuracy) / 10);
        if (a > 0) return 0;
        return a;
    }

}
public static class VectorExpansion
{
    public static Vector2Int ToVector2IntXZ(this Vector3 vec)
    {
        return new Vector2Int((int)Mathf.Round(vec.x), (int)Mathf.Round(vec.z));
    }
    public static Vector3 ToVector3XZ(this Vector2Int vec, float y)
    {
        return new Vector3(vec.x, y, vec.y);
    }
    public static int CompareGridPosValue(this Vector3 main, Vector3 target)
    {
        Vector2Int vec = main.ToVector2IntXZ() - target.ToVector2IntXZ();
        return Mathf.Abs(vec.x) + Mathf.Abs(vec.y);
    }
    public static int CompareGridPosValue(this Vector2Int main, Vector2Int target)
    {
        Vector2Int vec = main - target;
        return Mathf.Abs(vec.x) + Mathf.Abs(vec.y);
    }

}

