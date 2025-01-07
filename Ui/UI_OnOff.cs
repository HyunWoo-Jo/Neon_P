using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OnOff : MonoBehaviour
{
    private bool uiSet = false;

    public void Ui_OnOff(GameObject uiBackGround)
    {
        if(uiSet == true)
        {
            uiBackGround.SetActive(false);
            uiSet = false;
        }
        else
        {
            uiBackGround.SetActive(true);
            uiSet = true;
        }

    }
}
