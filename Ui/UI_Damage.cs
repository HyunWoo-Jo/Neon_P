using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Damage : MonoBehaviour, IValueEnter
{
    [SerializeField]
    private Image back;
    [SerializeField]
    private Image lightning;
    [SerializeField]
    private Text _text;

    void Init()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        back.material.SetFloat("_SpriteFade", 0);
        back.material.SetFloat("Pixel_Fade", 0);

        Color _color = lightning.color;
        _color.a = 0;
        lightning.color = _color;

        _color = _text.color;
        _color.a = 0;
        _text.color = _color;
    }

    public void Enter(int damage)
    {
        _text.text = damage.ToString();
        Init();
        StartCoroutine(Action());
    }
    
    IEnumerator Action()
    {
        Color lightColor = lightning.color;
        Color textColor = _text.color;

        for(float i = 1; i <= 10; i++)
        {
            back.fillAmount = i * 0.1f;
            back.material.SetFloat("_SpriteFade", i * 0.1f);

            lightning.fillAmount = i * 0.1f;
            lightColor.a = i * 0.1f;
            lightning.color = lightColor;

            textColor.a = i * 0.1f;
            _text.color = textColor;
            yield return new WaitForSeconds(0.03f);
        }
        for(float i = 1; i<= 20; i++)
        {
            back.material.SetFloat("Pixel_Fade", i * 0.04f);
            yield return new WaitForSeconds(0.02f);
        }
        for(float i= 40; i <= 50; i++)
        {
            back.material.SetFloat("Pixel_Fade", i * 0.02f);
            yield return new WaitForSeconds(0.05f);
        }
        for (float i = 9; i > 0; i--)
        {
            yield return new WaitForSeconds(0.04f);
            back.material.SetFloat("_SpriteFade", i * 0.1f);

            lightColor.a = i * 0.1f;
            lightning.color = lightColor;

            textColor.a = i * 0.1f;
            _text.color = textColor;
            

        }
        Destroy(gameObject);
    }





}
