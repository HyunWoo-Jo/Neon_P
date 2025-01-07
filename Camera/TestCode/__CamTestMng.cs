using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class __CamTestMng : MonoBehaviour
{

    public List<Transform> target;
    public Dictionary<string,Transform> m_targetTransform = new Dictionary<string,Transform>();
    public Dictionary<string,CameraSettingModel> m_viewSetting = new Dictionary<string,CameraSettingModel>();
    public Text viewText;
    public Text targetText;
    public Transform targetScroll;
    public Transform viewScroll;

    private bool isBattle = false;

    GameObject buttonObj = null;

    //private void Awake()
    //{
    //    m_viewSetting.Add("MainView", new CameraSettingModel(5, 0, 0.0f, 0.0f));
    //    m_viewSetting.Add("QuaterView", new CameraSettingModel(3, 10, 1.0f, 1.0f));
    //    m_viewSetting.Add("BackView", new CameraSettingModel(3, 1, 2.0f, 2.0f));
    //    for (int i = 0; i < target.Count; i++)
    //    {
    //        m_targetTransform.Add("Target" + (i + 1), target[i]);
    //        button = Resources.Load<GameObject>("Test/Button");
    //        Instantiate(button);
    //        button.GetComponent<RectTransform>().SetParent(targetScroll);
    //        button.GetComponentInChildren<Text>().name = "Target" + (i + 1);
    //    }
    //}

    void Start()
    {
       
        setTargetList();
        __CameraMng.Instance.Init();
        __CameraMng.Instance.setTestState(m_targetTransform[targetText.text],m_viewSetting["QuaterView"]);
      }

    private void Update()
    {
       __CameraMng.Instance.setTestState(m_targetTransform[targetText.text], m_viewSetting[viewText.text]);

    }


    private void setTargetList()
    {
        Text curText = null;
        for (int i = 0; i < target.Count; i++)
        {
            m_targetTransform.Add("Target" + (i + 1), target[i]);
            buttonObj = Resources.Load<GameObject>("Test/Button");
            GameObject button = Instantiate(buttonObj);
            button.transform.SetParent(targetScroll);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120 - (i * 30));
            curText = button.GetComponentInChildren<Text>();
            curText.text = "Target" + (i + 1);
            button.name = "Target" + (i + 1);
            button.GetComponent<Button>().onClick.AddListener(() => { this.gameObject.GetComponent<__CamTestMng>().OnChangeText(curText); });
        }
    }

    private void setViewList()
    {
        m_viewSetting.Add("MainView", new CameraSettingModel(5, 0, 0.0f, 0.0f));
        m_viewSetting.Add("QuaterView", new CameraSettingModel(3, 10, 1.0f, 1.0f));
        m_viewSetting.Add("BackView", new CameraSettingModel(3, 1, 2.0f, 2.0f));
    }


    public void OnChangeText(Text curText)
    {
        if (curText.text.Substring(0, 1).Equals('T')) targetText.text = curText.text;
        else if (curText.text.Substring(0, 1).Equals('V')) viewText.text = curText.text;
    }
}
