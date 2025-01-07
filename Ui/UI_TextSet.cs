using UnityEngine;
using UnityEngine.UI;
public class UI_TextSet : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    public void Enter(int value)
    {
        _text.text = value.ToString();
    }
}
