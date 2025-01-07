using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    private CharacterModel p_state;
    public GameObject enemy;
    // 안쓰는 변수 제거
    void Start()
    {
        p_state = gameObject.GetComponent<CharacterModel>();
        p_state.reloadBullet();
        p_state.setNewCurHp();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackEnemy(enemy);
        }
    }
    // 적 공격

    // 조현우 ==> 파라미터 useBullet 제거
    // 조현우 ==> 파라미터 CharacterModel e_state 제거
    public void AttackEnemy(GameObject enemy)
    {
        //변경
        CharacterModel e_state = enemy.GetComponent<CharacterModel>();
        //

        if (e_state.getCurHp() <= 0)
        {
            e_state.setNewCurHp();
        }
        if (p_state.getCurBullet() == 0)
        {
            Debug.Log("Reloading!");
            p_state.setIsBulletEmpty(false);
        }
        else
        {
            // 
            UsedBullet();
            Debug.Log("p_curBullet : " + p_state.getCurBullet());
            Hit(p_state.getDam(), Distance(enemy), e_state);
            Debug.Log("e_curHp: " + e_state.getCurHp());
            if (p_state.getCurBullet() == 0)
                p_state.setIsBulletEmpty(true);
        }
    }

    // 거리계산
    private float Distance(GameObject enemy)
    {
        float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
        Debug.Log(gameObject.name + ", " + enemy.name + " Distance: " + distance);
        return distance;
    }

    // 데미지 계산
    private void Hit(int _dmg, float _distance, CharacterModel e_state)
    {
        if (_distance > 6f)
        {
            _dmg -= 1;
        }
        e_state.setCurHp(e_state.getCurHp() - (_dmg - e_state.getShield()));
        // 주석처리
        //p_state.setCurBullet(useBullet);
    }

    private int UsedBullet()
    {
        p_state.setCurBullet(1);
        Debug.Log("usedBullet" + p_state.getCurBullet());
        return p_state.getCurBullet();
    }

    public void Reloading()
    {
        p_state.reloadBullet();
        Debug.Log("curBullet" + p_state.getCurBullet());
    }
}
