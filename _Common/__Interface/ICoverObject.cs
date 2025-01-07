using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoverObject
{
    float GetHight();
    Vector2Int[] GetCoverPoint();
    Vector2Int Position();
}
