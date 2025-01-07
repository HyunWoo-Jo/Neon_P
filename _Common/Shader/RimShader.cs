using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShaderStr {
    public bool isBase = true;
    public string baseT = "_BaseColorMap";
    public bool isBaseColor = true;
    public string baseColor = "_BaseColor";
    public bool isNormal = true;
    public string normal = "_NormalMap";
    public bool isMask = true;
    public string mask = "_MaskMap";
}

public class RimShader : MonoBehaviour
{
    [SerializeField]
    private bool isTest = false;

    [SerializeField]
    private ShaderStr shaderStr;
    [SerializeField]
    private Material rimMaterial;
    [SerializeField]
    private int index;
    private Material[] bufferMaterias;
    private Renderer _renderer;
    private bool isChange = false;
    
    public bool IsChange
    {
        set
        {
            if (isChange != value)
            {
                isChange = value;
                Material[] buffer = _renderer.materials;
                _renderer.materials = bufferMaterias;
                bufferMaterias = buffer;
            }
        }
        get { return isChange; }
    }

    private void Start()
    {
        if (isTest) IsChange = true;
    }

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _renderer = GetComponent<Renderer>();
        Material targetMaterial = _renderer.materials[index];

        if(shaderStr.isBase) rimMaterial.SetTexture("_BaseTex", targetMaterial.GetTexture(shaderStr.baseT));
        
        if(shaderStr.isBaseColor) rimMaterial.SetColor("_BaseColor", targetMaterial.GetColor(shaderStr.baseColor));
        if(shaderStr.isNormal)  rimMaterial.SetTexture("_Normal", targetMaterial.GetTexture(shaderStr.normal));
        if(shaderStr.isMask) rimMaterial.SetTexture("_Mask", targetMaterial.GetTexture(shaderStr.mask));

        Material[] inputMaterials = new Material[_renderer.materials.Length];
        Material changeRim = rimMaterial;

        for(int i = 0; i < _renderer.materials.Length; i++)
        {
            if (i.Equals(index))
            {
                inputMaterials[i] = rimMaterial;
                continue;
            }
            inputMaterials[i] = _renderer.materials[i];
        }
        bufferMaterias = inputMaterials;
        isChange = false;
    }

}
