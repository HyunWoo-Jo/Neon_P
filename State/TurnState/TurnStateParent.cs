using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Turn
{
    public class TurnStateParent : State
    {
        protected static TurnManager owner;

        protected virtual void Awake()
        {
            owner = this.gameObject.GetComponent<TurnManager>();
        }
        public override void Enter()
        {

        }
        public override void Exit()
        {

        }
    }
}