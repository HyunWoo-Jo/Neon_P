
using UnityEngine;


public class NodeTool : Node
{
#if UNITY_EDITOR
    [HideInInspector]
    public EditorData editorData;
    private GameObject unitObj;

    public void Renew()
    {
        GetComponent<Renderer>().material = editorData._material[(int)nodeData.nodeType];
        if (nodeData.unitType != UnitType.Empty)
        {
            DestroyImmediate(unitObj);
            unitObj = Instantiate(editorData._unit[(int)nodeData.unitType]);
            unitObj.transform.SetParent(this.gameObject.transform);
            unitObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            DestroyImmediate(unitObj);
        }

    }
#endif


}
