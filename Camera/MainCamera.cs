using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : _CameraScript
{

    public CameraSettingModel m_curCameraSetting;

    public CamPivot camPivot;
    private Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public Transform getTarget() { return target; }


    private void Awake()
    {
        m_cameraHandler = transform.parent;
    }

    private void LateUpdate()
    {
        moveCamera();
    }


    private void moveCamera()
    {
        if (Vector3.Distance(camPivot.getTransform().position,target.position) <= 14.501f) __CameraMng.Instance.isMoveFinished = true;
        if (__CameraMng.Instance.m_sceneState == __CameraMng.SceneState.BATTLE)
        {
            if (__CameraMng.Instance.isMoveFinished)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    __CameraMng.Instance.isMoveCamera = false;
                }
                else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    __CameraMng.Instance.isMoveCamera = true;
                    camPivot.MapMoving();
                }
            }
        }
        if (target != null)
        {
            getCamToTarget();
        }
    }


    private void getCamToTarget()
    {
        if (!__CameraMng.Instance.isMoveCamera)
        {
            camPivot.setTransfrom(target, __CameraMng.Instance.isTarRotTracking);
        }
        var wantedRotationAngle = camPivot.getTransform().eulerAngles.y;
        var wantedHeight = camPivot.getTransform().position.y + m_curCameraSetting.getHeight();


        var currentRotationAngle = camPivot.getTransform().eulerAngles.y;
        var currentHeight = m_cameraHandler.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, m_curCameraSetting.getHeiDamping() * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        m_cameraHandler.position = camPivot.getTransform().position;
        m_cameraHandler.position -= currentRotation * Vector3.forward * m_curCameraSetting.getDistance();

        // Set the height of the camera
        m_cameraHandler.position = new Vector3(m_cameraHandler.position.x, currentHeight, m_cameraHandler.position.z);

        // Always look at the target
        m_cameraHandler.LookAt(camPivot.getTransform());
    }

}
