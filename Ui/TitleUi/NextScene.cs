using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string sceneName;
    private string loading = "Loading";

    public void ButtonDown()
    {
        NowLoading.sceneName = sceneName;
        StartCoroutine(WaitforFade());
    }

    IEnumerator WaitforFade()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(loading);
    }
}
