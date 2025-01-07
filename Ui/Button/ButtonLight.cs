
using UnityEngine;
using UnityEngine.UI;
public abstract class ButtonLight : MonoBehaviour
{
    protected UI_Button ui_button;
    [SerializeField]
    protected GameObject lighting;
    [SerializeField]
    protected Image icon;

    protected virtual void Awake()
    {
        ui_button = GetComponentInParent<UI_Button>();
        ui_button.reset_listener += ResetUI;
    }
    protected virtual void Start()
    {
        CallBack.turn.start_listener += ResetUI;
    }
    protected virtual void ResetUI()
    {
        lighting.SetActive(false);
        icon.color = Color.white;
    }
    public abstract void OnButton();
    protected abstract bool ReturnChk();
}
