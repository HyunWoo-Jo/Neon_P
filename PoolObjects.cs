using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public GameObject _object;
    public Queue<GameObject> objQue = new Queue<GameObject>();
    public int count;
    protected virtual void Awake()
    {
        Add(this.count);
        _object.SetActive(false);
    }
    public void PayBackObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.gameObject.transform);
        objQue.Enqueue(obj);
    }
    public GameObject BorrowObject()
    {
        if (objQue.Count == 0)
        {
            Add(1);
        } 
        return objQue.Dequeue();
    }
    protected virtual void Add(int count)
    {    
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(_object);
            obj.transform.SetParent(this.gameObject.transform);
            objQue.Enqueue(obj);
        }
    }
}
