using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __CameraMng : SingletonObjects<__CameraMng>
{
    public enum SceneState
    {
        NONE,
        MAIN,
        BATTLE,
        ATTACK,
        Test
    }

    public SceneState m_sceneState = SceneState.NONE;

    public bool isMoveCamera;
    public bool isTarRotTracking;
    public bool isMoveFinished;

    public Dictionary<string, CameraSettingModel> m_cameraSetting = new Dictionary<string, CameraSettingModel>();



    public MainCamera m_mainCamera;


    public void Init()
    {
        m_cameraSetting.Add("MainView", new CameraSettingModel(5, 0, 0.0f, 0.0f));
        m_cameraSetting.Add("BattleView", new CameraSettingModel(3, 10, 1.0f, 1.0f));
        m_cameraSetting.Add("AttackView", new CameraSettingModel(3, 1, 2.0f, 2.0f));
        m_mainCamera = GameObject.Find("MainCamHandler").transform.GetChild(0).GetComponent<MainCamera>();
    }




    #region SetSceneState
    public void setMainState(Transform target)
    {
        if (m_sceneState != SceneState.MAIN || m_mainCamera.getTarget() != target)
        {
            m_sceneState = SceneState.MAIN;
            m_mainCamera.setTarget(target);
            m_mainCamera.m_curCameraSetting = m_cameraSetting["MainView"];
            isMoveCamera = false;
            isTarRotTracking = false;
            isMoveFinished = false;
        }
    }

    public void setBattleState(Transform target)
    {
        if (m_sceneState != SceneState.BATTLE || m_mainCamera.getTarget() != target)
        {
            m_sceneState = SceneState.BATTLE;
            m_mainCamera.setTarget(target);
            m_mainCamera.m_curCameraSetting = m_cameraSetting["BattleView"];
            isMoveCamera = false;
            isTarRotTracking = false;
            isMoveFinished = false;
        }
    }

    public void setAttackState(Transform target)
    {
        if (m_sceneState != SceneState.ATTACK || m_mainCamera.getTarget() != target)
        {
            m_sceneState = SceneState.ATTACK;
            m_mainCamera.setTarget(target);
            m_mainCamera.m_curCameraSetting = m_cameraSetting["AttackView"];
            isMoveCamera = false;
            isTarRotTracking = true;
            isMoveFinished = false;
        }
    }
    #endregion

    #region TestCode
    public void setTestState(Transform target, CameraSettingModel viewSetting)
    {
        if (m_sceneState != SceneState.Test || m_mainCamera.getTarget() != target)
        {
            m_sceneState = SceneState.Test;
            m_mainCamera.setTarget(target);
            m_mainCamera.m_curCameraSetting = viewSetting;
            isMoveCamera = false;
            isTarRotTracking = true;
            isMoveFinished = false;
        }
    }
    #endregion


    #region LoadObejct
    public GameObject LoadCamera(string objectName)
    {
        GameObject dummy = null;
        GameObject obj = Resources.Load(string.Format("Camera/{0}", objectName), typeof(GameObject)) as GameObject;
        if (obj != null)
        {
            dummy = obj;
            dummy.transform.localScale = Vector3.one;
        }
        return dummy;
    }
    #endregion

}
