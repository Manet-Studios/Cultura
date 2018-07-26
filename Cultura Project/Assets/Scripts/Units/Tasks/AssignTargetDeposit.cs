using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units.Tasks
{
    public class AssignTargetDeposit : Action
    {
        private IGatherer gatherer;
        public SharedTransform target;
        public SharedVector2 targetPosition;

        public override void OnAwake()
        {
            gatherer = transform.GetComponent<IGatherer>();
        }

        public override TaskStatus OnUpdate()
        {
            gatherer.TargetDeposit = target.Value.GetComponent<Construction.ResourceDeposit>();
            if (gatherer.TargetDeposit != null) targetPosition.Value = target.Value.position;
            return gatherer.TargetDeposit != null ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}