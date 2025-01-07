using UnityEngine;

public class UI_WorldUiCreate : MonoBehaviour
{

    [SerializeField]
    private GameObject ui_obj;

    public void CreateUIBar(Vector3 pos, int damage)
    {
        GameObject hit_UI = Instantiate(ui_obj);
        pos.y += 2f;
        pos.x += 0.5f;
        pos.z += 0.5f;
        hit_UI.transform.position = pos;
        hit_UI.SetActive(true);
        hit_UI.GetComponent<IValueEnter>().Enter(damage);
    }


}
