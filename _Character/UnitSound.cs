using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitSound : MonoBehaviour
{
    [HideInInspector]
    public AudioSource instance;

    [Header("{Standard Attack,Hit,Die}")]
    public AudioClip[] clips;

    private void Awake()
    {
        instance = GetComponent<AudioSource>();
    }

    public void OneShot(int clip)
    {
        instance.PlayOneShot(clips[clip]);
    }
    public void OneShot(int clip, float delay)
    {
        StartCoroutine(Delay(clip, delay));
    }

    IEnumerator Delay(int clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        OneShot(clip);
    }

}
