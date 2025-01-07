using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public MouseCtrl.MouseType _mouseType;
    private ButtonEvent eventText;
    // Start is called before the first frame update
    //void Start()
    //{
    //    eventText = transform.FindChild("UI/gu04_001_move").GetComponent<ClickButton>();
    //}

    public void ButtonCkick()
    {
        if(_mouseType.Equals(GameManager.instance.mouseCtrl.mouseType))
        {
            GameManager.instance.mouseCtrl.mouseType = MouseCtrl.MouseType.Default;
            Debug.Log("same mouse Ctrl" + _mouseType + "  " + GameManager.instance.mouseCtrl.mouseType);
        }
        else if(!_mouseType.Equals(GameManager.instance.mouseCtrl.mouseType))
        {
            _mouseType = GameManager.instance.mouseCtrl.mouseType;
            Debug.Log("currMouseCtrl : " + GameManager.instance.mouseCtrl.mouseType);
        }
    }
}
