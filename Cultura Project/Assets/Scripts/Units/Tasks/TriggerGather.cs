using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Cultura.Units.Tasks
{
    public class TriggerGather : Conditional
    {
        public SharedVector2 targetPos;
        private IGatherer gatherer;

        public override void OnAwake()
        {
            gatherer = transform.GetComponent<IGatherer>();
        }

        public override TaskStatus OnUpdate()
        {
            if (gatherer.TargetDeposit != null)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}