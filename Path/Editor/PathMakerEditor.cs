using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathMaker))]
public class PathMakerEditor : Editor
{
    PathMaker maker;
    private void OnEnable()
    {
        maker = target as PathMaker;
        if(maker.transform.childCount != 0)
        {
            if (maker.path == null || maker.path.Count == 0)
            {
                Renew();
            }
        }
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        for (int i = 0; i < maker.path.Count; i++)
        {
            if (maker.path[i] == null) Renew();
        }
        maker.transform.position = maker.transform.position.ToVector2IntXZ().ToVector3XZ(maker.transform.position.y);
        if (GUILayout.Button("생성"))
        {
            Add(maker.pathPosition);
        }
        maker.fileName = EditorGUILayout.TextField("파일저장이름 Path_{0}", maker.fileName);
        if (GUILayout.Button("저장"))
        {
            Save();
        }
        maker.uP = EditorGUILayout.ObjectField("로드 데이터", maker.uP, typeof(UnitPath), true) as UnitPath;
        if (GUILayout.Button("로드"))
        {
            Load();
        } 
    }

    void Add(Vector2Int TargetPosition)
    {
        if (maker.path == null) maker.path = new List<GameObject>();
        GameObject obj = new GameObject();
        obj.AddComponent<BoxCollider>();
        obj.transform.SetParent(maker.transform);
        obj.transform.position = TargetPosition.ToVector3XZ(2);
        obj.name = string.Format("path {0}", maker.pathPosition);
        maker.path.Add(obj);
    }
    void Save()
    {
        UnitPath unitPath = CreateInstance<UnitPath>();
        List<Vector2Int> pathData = new List<Vector2Int>();
        for(int i = 0; i < maker.path.Count; i++)
        {
            pathData.Add(maker.path[i].transform.position.ToVector2IntXZ());
        }
        unitPath.data = pathData;
        AssetDatabase.CreateAsset(unitPath, string.Format("Assets/00.MapData/PathData/Path_{0}.asset", maker.fileName));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    void Load()
    {
        if(maker.uP != null)
        {
            for(int i = 0; i < maker.uP.data.Count; i++)
            {
                Add(maker.uP.data[i]);
            }
        }
    }
    public void Renew()
    {
        maker.path = new List<GameObject>();
        for (int i = 0; i < maker.transform.childCount; i++)
        {
            GameObject obj = maker.transform.GetChild(i).GetComponent<Transform>().gameObject;
            if (obj != null)
                maker.path.Add(obj);
        }
    }
}
