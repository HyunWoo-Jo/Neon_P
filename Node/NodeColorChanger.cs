using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeColorChanger : MonoBehaviour
{
    [SerializeField]
    private Material defultMat;
    [SerializeField]
    private Material redMat;
    bool isChange;
    private List<Renderer> nodeRenderer;



    public bool IsChange
    {
        get { return isChange;  }
        
        set { Change(value); }
    }
    private void Awake()
    {
        nodeRenderer = new List<Renderer>();
    }

    public void AddNode(Renderer _renderer)
    {
        if (_renderer == null) return;
        if (isChange) _renderer.material = redMat;
        nodeRenderer.Add(_renderer);
    }

    private void Change(bool isChange)
    {
        if (this.isChange.Equals(isChange)) return;
        Material mat = isChange ? redMat : defultMat;
        for(int i = 0; i < nodeRenderer.Count; i++)
        {
            nodeRenderer[i].material = mat;
        }
        this.isChange = isChange;

    }


}
