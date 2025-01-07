using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UI_NextTurnPanel : MonoBehaviour
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
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
        for (float i = 19; i >= 0; i--)
        {
            float value = i * 0.05f;
            img.material.SetFloat("_Hologram_Value_1", value);
            yield return new WaitForSeconds(0.02f);
        }
        for (float i = 19; i >= 0; i--)
        {
            col.a = i * 0.05f;
            img.color = col;
            yield return new WaitForSeconds(0.02f);
        }
        gameObject.SetActive(false);
    }
}
