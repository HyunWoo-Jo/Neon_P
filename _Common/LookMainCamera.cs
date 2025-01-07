using UnityEngine;

public class LookMainCamera : MonoBehaviour
{

    [SerializeField]
#pragma warning disable IDE0044 // 읽기 전용 한정자 추가
    private bool isXlock;
    [SerializeField]
    private bool isYlock;
    [SerializeField]
    private bool isZlock;
#pragma warning restore IDE0044 // 읽기 전용 한정자 추가

    private void Update()
    {

        Vector3 pos = transform.position - Camera.main.transform.position;
        this.transform.rotation = Quaternion.LookRotation(pos);
        Vector3 e = this.transform.eulerAngles;
        if (isXlock) e.x = 0f;
        if (isYlock) e.y = 0f;
        if (isZlock) e.z = 0f;
        this.transform.eulerAngles = e;
    }
}
