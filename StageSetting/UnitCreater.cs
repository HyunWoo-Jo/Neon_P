using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreater : MonoBehaviour
{
    public GameObject[] units;
    public Transform unitParent;
    public GameObject CreateUnit(Vector3 pos, UnitType type)
    {
        GameObject obj = Instantiate(units[(int)type]);
        obj.transform.SetParent(unitParent);
        obj.gameObject.transform.position = pos;

        return obj;
    }
}
