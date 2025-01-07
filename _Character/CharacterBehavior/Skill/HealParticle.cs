using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealParticle : MonoBehaviour
{
    [SerializeField]
    Light _light;
    [SerializeField]
    Renderer floor_wave;
    float power;
    public void Run()
    {
        StartCoroutine(Action());
    }
    IEnumerator Action()
    {
        power = 0;
        for(int i = 0; i < 50; i++)
        {
            floor_wave.material.SetFloat("_Distortion", power * 0.2f);
            _light.intensity = power * 100f;
            power = i * 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 50; i++)
        {
            floor_wave.material.SetFloat("_Distortion", power * 0.2f);
            power -= 0.02f;
            _light.intensity = power * 100f;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
