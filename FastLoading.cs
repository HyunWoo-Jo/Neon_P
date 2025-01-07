using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastLoading : MonoBehaviour
{
    public string str;
    IEnumerator Start()
    {
        yield return null;
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(str);
        asyncOper.allowSceneActivation = false;
        while (!asyncOper.isDone)
        {
            if (asyncOper.progress >= 0.9f)
            {
                asyncOper.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
