using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    [HideInInspector]
    public UI_TimeLine timeline;
    public UI_WorldUiCreate damageCreate;
    public UI_WorldUiCreate healCreate;
    [HideInInspector]
    public UI_Button button;

    public UI_Scope scope;

    public BoolHandeler playerUI_litener;

    [SerializeField]
    private GameObject nextTurnImg;

    private void Awake()
    { 
        instance = this;
        timeline = GetComponent<UI_TimeLine>();
        button = GetComponent<UI_Button>();
    }
    private void Start()
    {
        CallBack.turn.end_listener += delegate (float a) { StartCoroutine(DelayNextTurn(a)); };
    }
    IEnumerator DelayNextTurn(float delay)
    {
        float t = delay > 2 ? delay - 1f : delay;
        yield return new WaitForSeconds(t);
        nextTurnImg.SetActive(true);
    }


    [SerializeField]
    private GameObject UI_player;
 


    public void UI_playerSet(bool isOn)
    {
        UI_player.SetActive(isOn);
        playerUI_litener?.Invoke(isOn);
        button.ResetUIButton();

    }
}
