using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFX.SFX
{
    public class RocketAdapter : MonoBehaviour
    {
        public SFX_ControlledObject[] ControlledObjects;

        public Transform target;

        private void Awake()
        {
        }
        //Run(Transfomr target) 타겟도 지정할수 있도록 만들것
        public void Run(Transform target)
        {
            this.target = target;
            foreach (var controlledObject in ControlledObjects)
            {
                controlledObject.Setup();
                controlledObject.Run();
            }
        }
    }
}
