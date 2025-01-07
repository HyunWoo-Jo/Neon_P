using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillNodeCreater : MonoBehaviour
{
    public static SkillNodeCreater instance;
    [SerializeField]
    private OutLineCreater outLine;
    [SerializeField]
    private float hight;

    private PoolObjects pool;

    [SerializeField]
    private Transform skilNodeP;
    private List<GameObject> nodesList = new List<GameObject>();
    private static readonly int FLOOR_LAYER = 1 << 14;

    public delegate void GameObjectsHandler(Vector3 pos ,List<GameObject> objs);
    public event GameObjectsHandler inputMouseDown_listener;

    private void Awake()
    {
        pool = GetComponent<PoolObjects>();
        instance = this;
    }

    public void Create(int cost)
    {
        AddPath(cost);
        StartCoroutine(Tracking());
    }

    public void Delete()
    {
        StopAllCoroutines();
        while(nodesList.Count != 0)
        {
            pool.PayBackObject(nodesList[0]);
            nodesList.RemoveAt(0);
        }
    }
    
    private void AddPath(int cost)
    {
        Queue<Vector2Int> path = new Queue<Vector2Int>();
        for(int x = -cost; x <= cost; x++)
        {
            for(int y=  -cost; y<= cost; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (Vector2Int.zero.CompareGridPosValue(pos) > cost) continue;
                path.Enqueue(pos);
            }
        }
        while(path.Count != 0)
        {
            AddNode(path.Dequeue());
        }
    }
    private void AddNode(Vector2Int pos)
    {
        GameObject obj = pool.BorrowObject();
        obj.transform.SetParent(skilNodeP);
        obj.transform.localPosition = pos.ToVector3XZ(0f);
        obj.SetActive(true);
        nodesList.Add(obj);
    }

    IEnumerator Tracking()
    {
        while (true)
        {
            bool isHIt = false;
            Vector2Int pos = GameManager.instance.mouseCtrl.GetLayContectPos(out isHIt).ToVector2IntXZ();
            if (isHIt) { 
                if (outLine.path.closeNode.ContainsKey(pos))
                {
                    skilNodeP.gameObject.SetActive(true);
                    Vector3 targetPos = pos.ToVector3XZ(hight);
                    skilNodeP.transform.position = targetPos;
                    MouseInputChk(targetPos);
                }
            } else
            {
                skilNodeP.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
    private void MouseInputChk(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<GameObject> objs = new List<GameObject>();
            for(int i = 0; i < nodesList.Count; i++)
            {
                foreach (var item in Stage.Instance.unitData.enemyUnitDic)
                {
                    if (nodesList[i].transform.position.ToVector2IntXZ() == item.Key.transform.position.ToVector2IntXZ())
                    {
                        objs.Add(item.Key);
                    }
                }
                foreach (var item in Stage.Instance.unitData.playerUnitDic)
                {
                    if (nodesList[i].transform.position.ToVector2IntXZ() == item.Key.transform.position.ToVector2IntXZ())
                    {
                        objs.Add(item.Key);
                    }
                }
            }
            SkillGenerate(pos + new Vector3(0,0.2f,0), objs);
        }
    }
    public void SkillGenerate(Vector3 pos, List<GameObject> list)
    {
        inputMouseDown_listener?.Invoke(pos, list);
    }

}
