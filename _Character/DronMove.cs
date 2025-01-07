using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronMove : MonoBehaviour
{
    public Transform target;

    public float hight;
    public float zPos;
    public float speed;

    [SerializeField]
    private AudioClip[] _clips;
    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        //transform.SetParent(GameManager.instance.gameObject.transform);
        buffer = speed;
        Standard();
    }

    public Vector3 addPos;
    private float buffer;
    public void Standard()
    {
        speed = buffer;
        addPos = new Vector3(0.5f, hight, zPos );
    }
    public void Attack()
    {
        _audio.PlayOneShot(_clips[Random.Range(0, _clips.Length)],1);
        addPos = new Vector3(0, hight + 1f, 0f);
        buffer = speed;
        speed = 3f;
    }


    void LateUpdate()
    {
        Vector3 pos = target.position + addPos;
        
        this.transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
