using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units.Tasks
{
    public class DepositResources : Action
    {
        private IDepositor unit;

        public override void OnAwake()
        {
            unit = transform.GetComponent<IDepositor>();
        }

        public override TaskStatus OnUpdate()
        {
            unit.DepositResources();
            return TaskStatus.Success;
        }
    }
}