using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePool : PoolObjects
{
    public static NodePool instance;
    

    protected override void Awake()
    {
        instance = this;
        base.Awake();
        _object.SetActive(false);
    }

    protected override void Add(int count)
    {      
        for (int i = 0; i < count; i++)
        {           
            GameObject obj = Instantiate(_object);
            obj.transform.SetParent(this.gameObject.transform);
            objQue.Enqueue(obj);
            GameManager.instance.nodeColorChanger.AddNode(obj.GetComponent<Renderer>());
        }
    }

    
}
