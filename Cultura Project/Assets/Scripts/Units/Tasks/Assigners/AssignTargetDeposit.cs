using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Units.Modules;

namespace Cultura.Units.Tasks
{
    public class AssignTargetDeposit : VillagerAction
    {
        private GathererModule gatherer;
        public SharedTransform target;
        public SharedVector2 targetPosition;

        public override void OnAwake()
        {
            base.OnAwake();
            gatherer = baseModule.GetModule<GathererModule>(ModuleID.Gatherer);
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                gatherer.TargetDeposit = target.Value.GetComponent<Construction.ResourceDeposit>();

                if (gatherer.TargetDeposit == null) return TaskStatus.Failure;

                targetPosition.Value = target.Value.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}