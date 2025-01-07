using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLight : MonoBehaviour
{
    private Light _light;
    float timer = 0;
    
    [SerializeField]
    private float maxLightningTime = 1f;
    [SerializeField]
    private float light_Intensity = 100;
    [SerializeField]
    AnimationCurve curve;
    bool isOff = false;
    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    private void OnEnable()
    {
        Init();
    }
    void Init()
    {
        timer = 0;
        isOff = false;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        if(!isOff)
        {
            _light.intensity = curve.Evaluate(timer/maxLightningTime) * light_Intensity;
            if (maxLightningTime < timer)
            {
                isOff = true;
                timer = 0;
            }
        } else
        {
            float intensity = _light.intensity = curve.Evaluate(1 - timer / maxLightningTime) * light_Intensity; 
            if(intensity <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
