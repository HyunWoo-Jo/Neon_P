using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    private Image hp_img;

    private void Awake()
    {
        hp_img = GetComponent<Image>();

    }
    private void Start()
    {
        CallBack.battle.hit_listener += Hit;
    }

    private void OnEnable()
    {
        StartCoroutine(RecoverHP());
    }

    IEnumerator RecoverHP()
    {
        
        while (TurnManager.instacne == null) yield return null;
        while (TurnManager.instacne.currentTurnUnit == null) yield return null;
        hp_img.fillAmount = 0f;
        CharacterModel model = TurnManager.instacne.currentTurnUnit.GetComponent<CharacterModel>();
        float fll = (float)model.getCurHp() / (float)model.getHp();
        fll /= 10f;
        for (int i = 0; i < 10; i++)
        {
            hp_img.fillAmount += fll;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Hit(GameObject obj)
    {
        CharacterModel model =  obj.GetComponent<CharacterModel>();
        StopAllCoroutines();

        float fll = (float)model.getCurHp() / (float)model.getHp();

        hp_img.fillAmount = fll;


    }


}
