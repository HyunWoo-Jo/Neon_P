using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    private Transform tr;
    private Transform target;
    private Vector3 position2Target = Vector3.zero;
    private Vector3 quakeStart;
    private bool isQuake = false;
    private bool isParent = false;
    private bool isOn = false;
    private bool isCreate = false;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    private void OnDisable()
    {
        isOn = false;
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        isOn = true;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        if (isQuake) return;
        Vector3 targetVec = target.position + position2Target;
        if (isParent) targetVec = position2Target;
        tr.localPosition = targetVec;
        tr.rotation = Quaternion.LookRotation(target.position - tr.position);
    }
    public void On_SubCam(Transform target,Vector3 addPos)
    {
        this.target = target;
        position2Target = addPos;
        __CameraMng.Instance.m_mainCamera.gameObject.SetActive(false);
        UI_Manager.instance.UI_playerSet(false);
        this.gameObject.SetActive(true);
    }
    public void SetParent(Transform parent)
    {
        isParent = true;
        transform.SetParent(parent);
    }
    public void ResetParent()
    {
        isParent = false;
        transform.SetParent(GameManager.instance.transform);
    }
    public void Off_SubCam()
    {
        this.gameObject.SetActive(false);
        __CameraMng.Instance.m_mainCamera.gameObject.SetActive(true);
        if (isCreate) Destroy(target.gameObject);
    }
    public void Off_SubCam(float delay)
    {
        StartCoroutine(OffCam(delay));
    }
    IEnumerator OffCam(float delay)
    {
        yield return new WaitForSeconds(delay);
        Off_SubCam();
    }
    public void Quake(float power,float time,float delay)
    {
        quakeStart = transform.position;
        if (!isOn) return;
        StartCoroutine(CameraQuake(power,time, delay));
    }
    IEnumerator CameraQuake(float power, float time, float delay)
    {
        float timer = 0;
        isQuake = true;
        while (true)
        {
            timer += Time.deltaTime;
            if(timer > time)
            {
                isQuake = false;
                yield break;
            }
            this.transform.position = quakeStart;
            Vector3 vec = Random.insideUnitSphere;
            vec.z = 0;
            transform.localPosition = vec * power + transform.position; 
            yield return new WaitForSeconds(delay);
        }

    }

}
