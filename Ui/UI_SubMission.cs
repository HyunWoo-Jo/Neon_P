using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SubMission : MonoBehaviour
{
    private bool isClear;
    private bool isAction;
    [SerializeField]
    private Color clearColor;
    [SerializeField]
    Image notClearImg;
    [SerializeField]
    Image clearImg;
    [SerializeField]
    private Text _text;


    private void OnEnable()
    {
        if (isAction)
        {
            notClearImg.gameObject.SetActive(false);
            clearImg.gameObject.SetActive(true);

            clearColor.a = 1;
            _text.color = clearColor;
            notClearImg.color = clearColor;

            isAction = false;
        }
    }

    public void MissionClear()
    {
        StartCoroutine(Clear());
    }
    IEnumerator Clear()
    {
        isClear = true;
        isAction = true;
        for(float i = 9;i>=0;i--)
        {
            _text.color = new Color(1,1,1, i / 10);
            notClearImg.color = new Color(1, 1, 1, i / 10);
            yield return new WaitForSeconds(0.02f);
        }
        notClearImg.gameObject.SetActive(false);
        clearImg.gameObject.SetActive(true);

        for (float i = 1; i <= 10; i++)
        {
            clearColor.a = i / 10;
            _text.color = clearColor;
            notClearImg.color = clearColor;
            yield return new WaitForSeconds(0.02f);
        }
        isAction = false;
    }

    


}
