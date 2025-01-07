using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPivot : MonoBehaviour
{

    public float camMoveSpeed = 3.0f;

    private Dictionary<KeyCode, Action> mapMoveDictionary;
    private delegate void Action();

    private Transform pivotTransform;

    private void Awake()
    {
        pivotTransform = GetComponent<Transform>();
        mapMoveDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyDown_W },
            { KeyCode.A, KeyDown_A },
            { KeyCode.S, KeyDown_S },
            { KeyCode.D, KeyDown_D }
        };

    }


    public void setTransfrom(Transform target, bool isRotTracking)
    {
        pivotTransform.position = Vector3.Lerp(pivotTransform.position, new Vector3(target.position.x, target.position.y + 1.5f, target.position.z), 1.0f * Time.deltaTime);
        if (isRotTracking) pivotTransform.rotation = target.rotation;
        else pivotTransform.rotation = Quaternion.identity;
    }

    public Transform getTransform() { return pivotTransform; }

    public void MapMoving()
    {
        if (Input.anyKey)
        {
            foreach (var dic in mapMoveDictionary)
            {
                if (Input.GetKey(dic.Key))
                {
                   dic.Value();
                }
            }
        }
    }

    private void KeyDown_W()
    {
        pivotTransform.position += DirectionVectorWallChk(Vector3.forward) * camMoveSpeed * Time.deltaTime;
    }

    private void KeyDown_S()
    {
        pivotTransform.position += DirectionVectorWallChk(Vector3.back) * camMoveSpeed * Time.deltaTime;
    }

    private void KeyDown_A()
    {
        pivotTransform.position += DirectionVectorWallChk(Vector3.left) * camMoveSpeed * Time.deltaTime;
    }

    private void KeyDown_D()
    {
        pivotTransform.position += DirectionVectorWallChk(Vector3.right) * camMoveSpeed * Time.deltaTime;
    }



    private Vector3 DirectionVectorWallChk(Vector3 direction)
    {
        Ray ray = new Ray(Camera.main.transform.position, direction);
        if (Physics.Raycast(ray, 1, LayerNumber.WALL)) return Vector3.zero;
        else return direction;

    }

}




 /*if (Input.GetKey(KeyCode.W))
                {
                    camPivot.position += Vector3.forward* camMoveSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    camPivot.position += Vector3.back* camMoveSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    camPivot.position += Vector3.left* camMoveSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    camPivot.position += Vector3.right* camMoveSpeed * Time.deltaTime;
                }
                
*/