using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasShader : MonoBehaviour
{
    Material buffer;
    [SerializeField]
    private Material mat;
    private Renderer _renderer;
    private float distance = 8f;
    bool isRun = false;
    [SerializeField]
    bool isSlowlyChange = false;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        buffer = _renderer.material;
    }
    public void Update()
    {

        if ((Camera.main.transform.position - this.transform.position).magnitude < distance)
        {
            if (isRun) return;
            if (isSlowlyChange)
            {
                StopAllCoroutines();
                StartCoroutine(Grass());
            } else
            {
                _renderer.material = mat;
            }
            isRun = true;
        }
        else
        {
            if (!isRun) return;
            if (isSlowlyChange)
            {
                StopAllCoroutines();
                StartCoroutine(ReturnOrigin());
            } else
            {
                _renderer.material = buffer;
            }
            isRun = false;
        }
    }
    IEnumerator Grass()
    {
        _renderer.material = mat;
        Color col = new Color(1f, 1f, 1f, 1f);
        _renderer.material.SetColor("_BaseColor", col);
        for(int i = 0; i< 60; i++)
        {
            col.a = 1f - i * 0.01f;
            _renderer.material.SetColor("_BaseColor", col);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator ReturnOrigin()
    {
        Color col = _renderer.material.GetColor("_BaseColor");
        for (int i = 0; i < 59; i++)
        {
            col.a += 0.01f;
            if (col.a >= 1f) break;
            _renderer.material.SetColor("_BaseColor", col);
            yield return new WaitForSeconds(0.01f);
        }
        _renderer.material = buffer;
    }
}
