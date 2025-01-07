using System.Collections;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    //캐릭터 코드
    [SerializeField]
    private int id;         //캐릭터 ID
    [SerializeField]
    private string _name;    //캐릭터 이름
    [SerializeField]
    private bool haveGun;   //true 비근접 /false 근접
    [SerializeField]
    private bool isEnemy;   //true AI캐릭터/false 플레이어캐릭터

    //기본스텟
    [SerializeField]
    private int agility; //민첩
    [SerializeField]
    private int hp; //최대hp
    [SerializeField]
    private int bullet; //최대총알갯수
    private int curHp;  //현재hp
    private int curBullet; //현재총알갯수

    //이동
    [SerializeField]
    private float moveSpeed;    //이동속도
    [SerializeField]
    private float rotSpeed;     //회전속도

    //전투관련
    [SerializeField]
    private int dammage;    //캐릭터 데미지
    [SerializeField]
    private int shield;     //캐릭터 방어력

    //범위
    [SerializeField]
    private int moveArea;   //이동가능범위
    [SerializeField]
    private int shootArea;  //사격가능범위


    protected virtual void Awake()
    {
        setNewCurHp();
        reloadBullet();
    }
    //상태
    private bool isDead;    //true 캐릭터죽어있음 /false 캐릭터살아있음
    private bool isBulletEmpty; //true 현재총알갯수 == 0 /false 현재총알갯수 != 0
    private bool isShieldBuff;  //true 방어력버프를 있는경우 /false 방어력버프가 없는경우

    public int getId() { return id; }
    public string getName() { return _name; }
    public bool IsHaveGun() { return haveGun; }
    public bool IsEnemy() { return isEnemy; }

    public int getAgility() { return agility; }
    public int getHp() { return hp; }
    public int getBullet() { return bullet; }

    public void setNewCurHp() { curHp = hp; }
    public void setCurHp(int curHp) { this.curHp = curHp; }
    public int getCurHp() { return curHp; }

    public void reloadBullet() { curBullet = bullet; }
    public void setCurBullet(int shootBulletCount) { curBullet -= shootBulletCount; }
    public int getCurBullet() { return curBullet; }

    public float getMoveSpeed() { return moveSpeed; }
    public float getRotSpeed() { return rotSpeed; }

    public int getDam() { return dammage; }
    public int getShield() { return shield; }

    public int getMoveArea() { return moveArea; }
    public int getShootArea() { return shootArea; }

    public void setIsDead(bool isDead) { this.isDead = isDead; }
    public void setIsBulletEmpty(bool isBulletEmpty) { this.isBulletEmpty = isBulletEmpty; }
    public void setIsShieldBuff(bool isShieldBuff) { this.isShieldBuff = isShieldBuff; }

    public bool IsDead() { return isDead; }
    public bool IsBulletEmpty() { return isBulletEmpty; }
    public bool IsShieldBuff() { return isShieldBuff; }

}
