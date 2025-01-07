using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Hologram : MonoBehaviour
{
    private Image img;
    [SerializeField]
    private bool isRun;
    private void Awake()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
        if (!isRun)
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(ShowAction());
    }

    IEnumerator ShowAction()
    {
        Color col = img.color;
        img.material.SetFloat("_Hologram_Value_1", 1);
        for (float i = 1; i <= 20; i++)
        {
            col.a = i * 0.05f;
            img.color = col;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        for (float i = 19; i >= 0; i--)
        {
            float value = i * 0.05f;
            img.material.SetFloat("_Hologram_Value_1", value);
            yield return new WaitForSeconds(0.02f);
        }
    }


}
