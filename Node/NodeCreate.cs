using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreate : MonoBehaviour
{
    public static NodeCreate instance;
    public PathFinder path = new PathFinder();
    public GameObject line;
    public bool isUse;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    Stack<GameObject> objStack = new Stack<GameObject>();
    private void Start()
    {
        CallBack.battle.nodeCreate_listener += NodeSet;
    }
    public void NodeDelete()
    {
        this.StopAllCoroutines();
        StartCoroutine(PayBack());
    }


    void NodeSet(Vector3 pos, int cost)
    {
        path.MoveAbleGridFind(pos, cost);
        StartCoroutine(Create(path.moveAbleNode_hashSet, pos.y));
    }
    IEnumerator Create(HashSet<Vector2Int> hashSet,float y)
    {
        isUse = true;
        foreach(var pos in hashSet) {
            GameObject obj = NodePool.instance.BorrowObject();
            obj.transform.position = new Vector3(pos.x, 1f, pos.y);
            objStack.Push(obj);
            obj.SetActive(true);
            yield return null;
        }
        yield return null;
    }
    IEnumerator PayBack()
    {
       
        while (objStack.Count != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                NodePool.instance.PayBackObject(objStack.Pop());
                if (objStack.Count == 0) yield break;
            }
            yield return null;
        }
    }
    public GameObject LineCreate(List<Vector2Int> list,float y)
    {
        GameObject toinstance = Instantiate(line);
        toinstance.SetActive(true);
        LineRenderer lineRender = toinstance.GetComponent<LineRenderer>();
        lineRender.positionCount = list.Count;

        for(int i = 0; i < list.Count; i++)
        {

            lineRender.SetPosition(i, list[i].ToVector3XZ(y));
        }
        
        return toinstance;

    }
    


}
