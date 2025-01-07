using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeTool))]
public class NodeEditor : Editor
{
    NodeTool instance;

    private void OnEnable()
    {
        instance = target as NodeTool;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("갱신"))
        {
            instance.Renew();
        }
    }
}
