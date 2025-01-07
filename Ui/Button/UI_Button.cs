
using UnityEngine;
using UnityEngine.UI;
public class UI_Button : MonoBehaviour
{
    public enum ButtonState
    {
        Notting,
        All,
        Move,
        Attack,
        Stay,
        Skil
    }

    public ButtonState currentButtonState = ButtonState.All;
    public VoidHandeler inputButton_listener; 
    public void InputButton()
    {
        inputButton_listener?.Invoke();
    }


    public VoidHandeler reset_listener;
    public void ResetUIButton()
    {
        reset_listener?.Invoke();
    }

}
