using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private enum Owner
    {
        Player,
        Enemy
    };
    [SerializeField]
    private Owner owner;
    [SerializeField]
    private float deleteTime = 3.5f;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject impactParticle;

    bool isHit = false;

    private int targetLayer;
    private Transform _transform;

    private PoolObjects pool;

    private void Awake()
    {
        _transform = this.transform;
        targetLayer = owner.Equals(Owner.Player) ? 0b111 << 10 : 0b111 << 11;
    }
    public void Enter(GameObject obj, PoolObjects pool)
    {
        this.pool = pool;
        gameObject.SetActive(true);
        transform.SetParent(GameManager.instance.transform);
        Vector3 vec = obj.transform.position;
        vec.y += 1.1f;
        vec -= _transform.position;
        _transform.rotation = Quaternion.LookRotation(vec);
        HitChk();
        //if(vec.magnitude <= 2f)
        //{
        //    HitAction hitA = obj.GetComponent<HitAction>();
        //    if (hitA != null)
        //    {
        //        hitA.Hit(this.gameObject.transform);
        //    }    
        //}
    }

    private void FixedUpdate()
    {
        _transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime, Space.Self);
        if (isHit) return;
        HitChk();
    }
    public void HitChk()
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, 1f, targetLayer))
        {
            HitAction hitA = hit.collider.GetComponent<HitAction>();
            if (hitA != null)
            {
                hitA.Hit(this.gameObject.transform);
            }
            GameObject impact = Instantiate(impactParticle);
            impact.transform.position = this.transform.position;
            impact.transform.rotation = this.transform.rotation;
            if (pool != null)
                pool.PayBackObject(this.gameObject);
        }
    }
    

    private void OnEnable()
    {
        StartCoroutine(AutoPool());
    }

    IEnumerator AutoPool()
    {      
        yield return new WaitForSeconds(deleteTime);
        pool.PayBackObject(this.gameObject);
    }

}
