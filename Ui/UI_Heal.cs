using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Heal : MonoBehaviour, IValueEnter
{
    [SerializeField]
    private float life_Time = 2f;
    public void Enter(int value)
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<UI_TextSet>().Enter(value);
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(life_Time);
        Destroy(this.gameObject);
    }

}
