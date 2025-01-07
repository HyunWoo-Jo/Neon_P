using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TimeLineMove : MonoBehaviour
{
    [SerializeField]
    private GameObject defaltUi;
    [SerializeField]
    private GameObject moveUi;
    [SerializeField]
    private float ui_speed;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private bool isTimeLineMove = true;

    public void Ui_Down()
    {
        StopAllCoroutines();
        StartCoroutine(UiDown());
    }

    public void Ui_Up()
    {
        StopAllCoroutines();
        StartCoroutine(UiUp());
    }

    private void OnEnable()
    {
        if (isTimeLineMove == true)
        {
            transform.localPosition = moveUi.transform.localPosition;
            StartCoroutine(UiDefalt());
        }
    }

    IEnumerator UiDown()
    {
        while (transform.localPosition != moveUi.transform.localPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveUi.transform.localPosition, ui_speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator UiUp()
    {
        while (transform.localPosition != defaltUi.transform.localPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaltUi.transform.localPosition, ui_speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator UiDefalt()
    {
        yield return new WaitForSeconds(waitTime);
        while (transform.localPosition != defaltUi.transform.localPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaltUi.transform.localPosition, ui_speed * Time.deltaTime);
            yield return null;
        }
    }
}
