using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_TimeLineSlot : MonoBehaviour
{
    private Image _image;
    public Image _p;
    SlotUnit data;
    [SerializeField]
    bool colorConst = false;
    public static Color playerColor;
    public static Color enemyColor;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetSlot(SlotUnit slot)
    {
        if (data.data.id.Equals(slot.data.id)) return;
        data = slot;
        _image.sprite = data.data.sprite;
        _image.material = data.data.material;
        if (!colorConst)
            _p.color = data.data.id - 100 < 0 ? playerColor : enemyColor;
    }
    public SlotUnit GetSlot()
    {
        return data;
    }


}
