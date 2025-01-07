using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage Instance;
    private List<List<NodeData>> grid;
    private Vector2Int maxGridSize;
    public Vector2Int MaxGridSize {
        get => maxGridSize;
        set => maxGridSize = value;
    }


    public List<List<NodeData>> Grid {
        get => grid;
        set => grid = value;
    }
    [HideInInspector]
    public UnitData unitData;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void OnDestroy()
    {
        Instance = null;
    }

}
