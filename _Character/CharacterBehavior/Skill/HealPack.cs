using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    [SerializeField]
    private int healRange;
    [SerializeField]
    private int healPower;
    [SerializeField]
    private float life_Time = 5f;
    List<CharacterModel> healList = new List<CharacterModel>();
    IEnumerator Start()
    {
        GameManager.instance.subcam.On_SubCam(this.transform, new Vector3(0, 7f, -2f));
        yield return new WaitForSeconds(0.5f);
        SerchPlayerCharacter();
        GetComponent<HealParticle>().Run();
        Heal();
        yield return new WaitForSeconds(life_Time);
        GameManager.instance.subcam.Off_SubCam();
        CallBack.turn.End(0.5f);
        Destroy(this.gameObject);
    }


    private void SerchPlayerCharacter()
    {
        healList.Clear();
        foreach(var item in Stage.Instance.unitData.playerUnitDic)
        {
            if((transform.position.ToVector2IntXZ().CompareGridPosValue(
                item.Value.transform.position.ToVector2IntXZ())) <= healRange){
                healList.Add(item.Value);
            }
        }
    }
    private void Heal()
    {
        for(int i = 0; i < healList.Count; i++)
        {
            healList[i].setCurHp(healList[i].getHp() + healPower);
            UI_Manager.instance.healCreate.CreateUIBar(healList[i].transform.position, healPower);
        }
    }

}
