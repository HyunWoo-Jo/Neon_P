using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPointInOut : MonoBehaviour
{
    [SerializeField]
    private Image panel;
    [SerializeField]
    private float defaltGamma;
    [SerializeField]
    private float gamma;

    private void OnDisable()
    {
        Color alpha = panel.color;
        alpha.a = defaltGamma;
        panel.color = alpha;
    }

    public void BtnPointEnter()
    {
        Color alpha = panel.color;
        alpha.a = gamma;
        panel.color = alpha;
    }

    public void BtnPointExit()
    {
        Color alpha = panel.color;
        alpha.a = defaltGamma;
        panel.color = alpha;
    }
}
