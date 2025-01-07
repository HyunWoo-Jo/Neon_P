using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToStage : MonoBehaviour
{
    public MapData mapData;
    public void Awake()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        while(Stage.Instance == null)
        {
            yield return null;
        }
        UnitData unitData = this.gameObject.GetComponent<UnitData>();
        UnitCreater unitCreater = this.gameObject.GetComponent<UnitCreater>();
        List<List<NodeData>> grid = new List<List<NodeData>>();
        Stage.Instance.MaxGridSize = new Vector2Int(20, 20);
        Dictionary<Vector2Int, UnitType> units = new Dictionary<Vector2Int, UnitType>();
        for(int x=0;x< 20;x++) {
            grid.Add(new List<NodeData>());
            for(int y=0; y< 20; y++) {
                grid[x].Add(new NodeData(new Vector2Int(x,y),NodeType.Standard,999,0,0));
                grid[x][y].isUse = true;
            }
        }
        for (int i = 0; i < mapData.data.Count; i++)
        {
            NodeData node = mapData.data[i];

            if (node.unitType != UnitType.Empty)
            {
                node.isUse = true;
                units.Add(node.pos, node.unitType);
            }
            node.isUse = false;
            grid[node.pos.x][node.pos.y] = node;
            
        }
        unitData.playerUnitDic = new Dictionary<GameObject, CharacterModel>();
        unitData.enemyUnitDic = new Dictionary<GameObject, AICharacterModel>();
        unitData.unitDic = new Dictionary<GameObject, CharacterModel>();
        foreach(var item in units)
        {
            GameObject obj = unitCreater.CreateUnit(item.Key.ToVector3XZ(1f), item.Value);
            // 검사부 수정해야됨
            CharacterModel model = obj.GetComponent<CharacterModel>();
            if (!(model is AICharacterModel))
            {
                unitData.playerUnitDic.Add(obj, model);
                unitData.unitDic.Add(obj, model);
            }
            else
            {
                unitData.enemyUnitDic.Add(obj, obj.GetComponent<AICharacterModel>());         
            }
        }
        Stage.Instance.unitData = unitData;
        Stage.Instance.Grid = grid;

        while (TurnManager.instacne == null)
        {
            yield return null;
        }
        CallBack.system.EndLoad();
        TurnManager.instacne.GameStart();
        Destroy(unitCreater);
        Destroy(this);
    }
}
public class Vector2IntCompareTo : IComparer<Vector2Int>
{
    public int Compare(Vector2Int aV, Vector2Int bV)
    {
        if (aV.x.Equals(bV.x) && aV.y.Equals(bV.y))
            return 0;
        int value = (aV.x * 100000 + aV.y) - (bV.x * 100000 + bV.y);
        if (value > 0)
            return 1;
        else
            return -1;
    }
}
