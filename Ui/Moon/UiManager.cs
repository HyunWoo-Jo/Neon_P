using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private CharacterModel p_model; // 현재 턴인 캐릭터의 CharacterModel
    private CharacterModel e_model;
    private GameObject player; // 현재 턴인 캐릭터
    public GameObject Ui;
    public GameObject player2;
    public GameObject melee;
    public GameObject range;
    public GameObject reloadingBrn;
    public GameObject attackBrn;
    public GameObject moveBrn;
    public Slider ammo;
    public Slider hpBar;
    private float dmg = 10f;

    void Awake()
    {
        //CallBack.battle.move_listener += table.move.Move;
    }
    void Start()
    {
        p_model = player2.GetComponent<CharacterModel>();
        p_model.reloadBullet();
        p_model.setNewCurHp();


    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        StartCoroutine("Ammo", player2);
    //    }
    //}

    private void WeaponKind(GameObject player)
    {
        p_model = player.GetComponent<CharacterModel>();
        if (p_model.IsHaveGun() == true)
        {
            range.SetActive(true);
            melee.SetActive(false);
            Debug.Log("range mode");
        }
        else
        {
            range.SetActive(false);
            melee.SetActive(true);
            Debug.Log("melee mode");
        }
    }

    //IEnumerator Ammo(GameObject player)
    //{
    //    p_model = player.GetComponent<CharacterModel>();
    //    ammo.maxValue = p_model.getBullet();
    //    for (int i = 0; i < 10; i++)
    //    {
    //        p_model.setCurBullet(1);
    //        ammo.value = p_model.getCurBullet();
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}

    IEnumerator HpBar(GameObject player)
    {
        p_model = player.GetComponent<CharacterModel>();
        hpBar.maxValue = p_model.getHp();
        for (int i = 0; i <= 10; i++)
        {
            p_model.setCurHp(p_model.getCurHp() - (e_model.getDam() / 10));
            hpBar.value = p_model.getCurHp();
            yield return new WaitForSeconds(0.05f);
        }
    }

    //private void UiOnOff()
    //{
    //    if(TurnManager.instacne.turn.)
    //    {

    //    }
    //}

    private void ReloadingUi(GameObject player)
    {
        p_model = player.GetComponent<CharacterModel>();
        if (p_model.getCurBullet() == 0)
        {
            attackBrn.SetActive(false);
            reloadingBrn.SetActive(true);
        }
    }

    public void Reloading(GameObject player)
    {
        player = player2;
        p_model.reloadBullet();
    }

    //private void Reset(GameObject player)
    //{
    //    player = getcom
    //    if(player.)
    //}
}
