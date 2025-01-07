using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreate : MonoBehaviour
{
    public GameObject nodeObj;
    public Vector2Int mapSize;
    [HideInInspector]
    public MapData loadData;
    [HideInInspector]
    public string fileName;
    public List<GameObject> nodeList;
}
