using UnityEngine;

public class MouseCtrl : MonoBehaviour
{
    public enum MouseType
    {
        EnemyTurn = 0,
        Default = 1,
        All = 2,
        Move = 3,
        Attack = 4,
        Stay = 5,
        Skil = 6
    }
    public MouseType mouseType;
    public bool isRun = true;

    private bool isTargetE;
    private GameObject targetE;
    [HideInInspector]
    public Vector2Int targetV = new Vector2Int(-11111,-11111);

    public void SetTarget(GameObject obj)
    {
        targetE = obj;
        isTargetE = true;
    }
    public void ResetTarget()
    {
        targetE = null;
        isTargetE = false;
    }

    private MouseType clearBuffer = MouseType.Stay;
    GameObject nodeRay;
    GameObject enemy;
    void Update()
    {
        if (!mouseType.Equals(clearBuffer))
        {
            Clear();
            clearBuffer = mouseType;
        }
        if (mouseType.Equals(MouseType.EnemyTurn) || mouseType.Equals(MouseType.Default)) return;

        if(mouseType.Equals(MouseType.Move) || mouseType.Equals(MouseType.All))
            TargetRayCreate(ref nodeRay, 0b1110, 9);

        if (mouseType.Equals(MouseType.Attack) || mouseType.Equals(MouseType.All))
            TargetRayCreate(ref enemy, 0b10110, 10);


    }
    #region __Clear__
    void Clear()
    {
        NothingHit(ref nodeRay);
        NothingHit(ref enemy);
    }

    #endregion
    #region __RayCreate__

    private void TargetRayCreate(ref GameObject target, int layer,int targetLayer)
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RayHitChk(ref target, ray, layer, targetLayer);
    }
    private void RayHitChk(ref GameObject target, Ray ray,int layer, int targetLayer)
    {
        if ((1 << (int)mouseType & layer) != 0 && isRun)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, 1 << targetLayer))
            {
                if (!isTargetE)
                {
                    LayerContectChk(hit, ref target);
                } else
                {
                    LayerContectChkTarget(hit, ref target);
                }
                return;
            }
        }
        NothingHit(ref target);
    }
    #endregion
    #region __ChkLogic__
    private void LayerContectChk(RaycastHit hit,ref GameObject targetObj)
    {
        RayNull2Target(hit,ref targetObj);
        RayObj2Target(hit, ref targetObj);
        RayMouseDown(hit, ref targetObj);
    }
    private void LayerContectChkTarget(RaycastHit hit, ref GameObject targetObj)
    {
        
        if (hit.collider.gameObject.GetInstanceID() != targetE.gameObject.GetInstanceID())
        {
            NothingHit(ref targetObj);
        }
        else
        {
            RayNull2Target(hit, ref targetObj);
        }
        
        RayObj2Target(hit, ref targetObj);
        RayMouseDown(hit, ref targetObj);
    
    }
    private void RayNull2Target(RaycastHit hit,ref GameObject targetObj)
    {
        if (targetObj == null)
        {
            IRayCastHit rayCastHit = hit.collider.GetComponent<IRayCastHit>();
            if (rayCastHit != null)
            {
                rayCastHit.RayEnter();
                targetObj = hit.collider.gameObject;
            }
        }
    }
    private void RayObj2Target(RaycastHit hit, ref GameObject targetObj)
    {
        if (targetObj == null) return;
        if (hit.collider.gameObject.GetInstanceID() != targetObj.GetInstanceID())
        {
            IRayCastHit rayCastHit = hit.collider.GetComponent<IRayCastHit>();
            // Enter
            if (rayCastHit != null)
            {
                targetObj.GetComponent<IRayCastHit>().RayExit();
                targetObj = hit.collider.gameObject;
                rayCastHit.RayEnter();
            }
        }
    }
    private void RayMouseDown(RaycastHit hit, ref GameObject targetObj)
    {
        if (targetObj != null)
        {
            IRayCastHit rayCastHit = hit.collider.GetComponent<IRayCastHit>();
            if (rayCastHit != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    rayCastHit.RayDown();
                }
            }
        }
    }
   
    private void NothingHit(ref GameObject targetObj)
    {
        if (targetObj == null) return;
        IRayCastHit rayCastHit = targetObj.GetComponent<IRayCastHit>();
        if (rayCastHit != null)
        {
            rayCastHit.RayExit();
            targetObj = null;
        }
    }

    public Vector3 GetLayContectPos(out bool isHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, float.MaxValue,1 << 14) && isRun)
        {
            isHit = true;
            if (isTargetE)
            {
                if (hit.point.ToVector2IntXZ().Equals(targetV))
                    return hit.point;
            }
            else
                return hit.point;
        }
        isHit = false;
        return Vector3.zero;
    }


    #endregion
}
