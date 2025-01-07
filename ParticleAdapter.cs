using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAdapter : MonoBehaviour
{
    private ParticleSystem[] _particles;
    private MeshRenderer[] _renderer;
    private AudioSource _audio;
    [SerializeField]
    private GameObject _light;
    [SerializeField]
    private AudioClip exp;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _particles = GetComponentsInChildren<ParticleSystem>();
        _renderer = GetComponentsInChildren<MeshRenderer>();
    }
    public virtual void Run()
    {
        _audio.PlayOneShot(exp);
        foreach(var item in _particles)
        {
            item.Play();
        }
        if (_light != null)
            _light.SetActive(true);
        
        foreach(var item in _renderer)
        {
            item.enabled = false;
        }
    }

}
