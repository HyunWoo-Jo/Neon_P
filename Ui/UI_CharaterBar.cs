using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharaterBar : MonoBehaviour
{
    private CharacterModel model;

    [SerializeField]
    private Slider slider_H;
    [SerializeField]
    private Image hpBar;
    
    [SerializeField]
    private Slider slider_M;
    [SerializeField]
    private Image magazineBar;
    [SerializeField]
    private Text magazinetext;

    [SerializeField]
    private Color playerColor;
    [SerializeField]
    private Color enemyColor;

    private void Awake()
    {
        model = GetComponentInParent<CharacterModel>();
    }
    private void Start()
    {
        if(UI_Manager.instance == null) return;
        UI_Manager.instance.playerUI_litener += SetObj;
        GetComponent<Canvas>().worldCamera = __CameraMng.Instance.m_mainCamera.GetComponent<Camera>();

        if (!model.IsHaveGun() || model.IsEnemy())
        {
            slider_M.gameObject.SetActive(false);
        }
        if (model.IsEnemy())
        {
            hpBar.color = enemyColor;
        }else
        {
            hpBar.color = playerColor;
            magazineBar.color = playerColor;
        }
        Renew();
    }

    private void OnDestroy()
    {
        UI_Manager.instance.playerUI_litener -= SetObj;
    }
    //private void Update()
    //{
    //    Renew();
    //}

    private void OnEnable()
    {
        if(model != null)
        {
            Renew();
        }
    }
    public void Renew()
    {
        RenewHit();
        RenewMagazine();
    }
    public void RenewHit()
    {      
        float hp = (float)model.getCurHp() / (float)model.getHp();
        slider_H.value = hp * 100f;
        if (model.IsDead()) Destroy(this.gameObject);

    }
    public void RenewMagazine()
    {
        if (model.IsEnemy()) return;
        magazinetext.text = model.getCurBullet().ToString();
        float ma = (float)model.getCurBullet() / (float)model.getBullet();
        slider_M.value = ma * 100f;
    }

    private bool SetObj(bool isOn)
    {
        this.gameObject.SetActive(isOn);
        return isOn;
    }

}
