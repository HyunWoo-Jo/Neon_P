using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OptionOnOff : MonoBehaviour
{

    UI_OnOff optionUi = new UI_OnOff();
    [SerializeField]
    private GameObject optionUiBackGround;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionUi.Ui_OnOff(optionUiBackGround);
        }
    }
}
