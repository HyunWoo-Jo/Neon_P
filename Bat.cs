using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    AudioSource _sound;
    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(10)){
            StrikTarget(other);
        }
    }
    
    void StrikTarget(Collider other)
    {
        other.gameObject.GetComponent<HitAction>().Hit(this.transform);
        other.gameObject.GetComponent<AttackUnit>().RealHit();
        _sound.PlayOneShot(_sound.clip);
    }
}
