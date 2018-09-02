using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units.Tasks
{
    public class AssignTargetDeposit : Action
    {
        /*
        private IGatherer gatherer;
        public SharedTransform target;
        public SharedVector2 targetPosition;

        public override void OnAwake()
        {
            gatherer = transform.GetComponent<IGatherer>();
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                gatherer.TargetDeposit = target.Value.GetComponent<Construction.ResourceDeposit>();
                targetPosition.Value = target.Value.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
        */
    }
}