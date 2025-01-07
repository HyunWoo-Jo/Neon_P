using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NowLoading : MonoBehaviour
{
    public Slider Progressbar;
    [HideInInspector]
    public static string sceneName;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        asyncOper.allowSceneActivation = false;
        while (!asyncOper.isDone)
        {
            yield return null;
            if (Progressbar.value < 0.9f)
            {
                Progressbar.value = Mathf.MoveTowards(Progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (Progressbar.value >= 0.9f)
            {
                Progressbar.value = Mathf.MoveTowards(Progressbar.value, 1f, Time.deltaTime);

            }
            if (Progressbar.value >= 1f && asyncOper.progress >= 0.9f)
            {
                asyncOper.allowSceneActivation = true;
            }
        }
    }
}
