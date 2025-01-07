using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapCreate))]
public class MapCreateEditor : Editor
{
    MapCreate instance;
    private void OnEnable()
    {
        instance = target as MapCreate;
        if (instance.gameObject.transform.childCount != 0)
        {
            if(instance.nodeList.Count == 0)
            {
                instance.nodeList = new List<GameObject>();
                for(int i = 0; i < instance.transform.childCount; i++)
                {
                    NodeTool tool = instance.transform.GetChild(i).GetComponent<NodeTool>();
                    if (tool != null)
                        instance.nodeList.Add(tool.gameObject);
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("생성"))
        {
            CreateMap();
        }
        instance.fileName = EditorGUILayout.TextField("파일저장이름", instance.fileName);
        if (GUILayout.Button("저장"))
        {
            SaveMapData();
        }
        instance.loadData = EditorGUILayout.ObjectField("로드 데이터", instance.loadData, typeof(MapData), true) as MapData;
        if (GUILayout.Button("로드"))
        {
            LoadMapData();
        }
        if (GUILayout.Button("삭제"))
        {
            DeleteMap();
        }

    }

    public void CreateMap()
    {
        if (instance.nodeList != null)
        {
            if(instance.nodeList.Count != 0)
            {
                Debug.Log("삭제후 생성");
                return;
            }
            instance.nodeList = new List<GameObject>();
            EditorData editData = instance.GetComponent<EditorData>();
  
            for (int x = 0; x < instance.mapSize.x; x++)
            {
                for (int z = 0; z < instance.mapSize.y; z++)
                {
                    GameObject obj = Instantiate(instance.nodeObj);
                    obj.transform.SetParent(instance.gameObject.transform);
                    Vector3 vec = instance.transform.position;
                    vec.x += x;
                    vec.z += z;
                    obj.transform.position = vec;
                    NodeTool tool = obj.GetComponent<NodeTool>();
                    tool.nodeData = new NodeData();
                    tool.editorData = editData;
                    tool.nodeData.cost = 1;

                    instance.nodeList.Add(obj);
                }
            }
        }
    }
    public void SaveMapData()
    {
        if (instance.fileName == "")
        {
            Debug.Log("저장 이름 입력");
        }
        else
        {
            MapData mapData = CreateInstance<MapData>();
            mapData.data = new List<NodeData>();

            for (int i = 0; i < instance.nodeList.Count; i++)
            {
                if (instance.nodeList[i] != null)
                {
                    NodeData toNodeData = instance.nodeList[i].GetComponent<Node>().nodeData;
                    toNodeData.pos = instance.nodeList[i].transform.position.ToVector2IntXZ();
                    mapData.data.Add(toNodeData);
                }
            }
            AssetDatabase.CreateAsset(mapData, string.Format("Assets/00.MapData/{0}.asset", instance.fileName));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    public void LoadMapData()
    {
        if (instance.loadData == null)
        {
            Debug.Log("데이터 입력");
            return;
        }
        if (instance.nodeList.Count != 0)
        {
            foreach (var item in instance.nodeList)
            {
                DestroyImmediate(item.gameObject);
            }
            instance.nodeList.Clear();
        }
        instance.nodeList = new List<GameObject>();
 

        EditorData editData = instance.GetComponent<EditorData>();

        foreach (var item in instance.loadData.data)
        {
            GameObject obj = Instantiate(instance.nodeObj);
            obj.transform.SetParent(instance.transform);
            obj.transform.position = item.pos.ToVector3XZ(instance.transform.position.y);
            NodeTool tool = obj.GetComponent<NodeTool>();
            tool.nodeData = item;
            tool.editorData = editData;
            tool.Renew();

            instance.nodeList.Add(obj);
        }
    }
    public void DeleteMap()
    {
        if (instance.nodeList.Count != 0)
        {
            foreach (var item in instance.nodeList)
            {
                DestroyImmediate(item.gameObject);
            }
            instance.nodeList.Clear();
        }
    }
}
