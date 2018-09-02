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

        public override void OnStart()
        {
            gatherer = baseModule.GetModule<GathererModule>(ModuleID.Gatherer);
            Debug.Log(gatherer);
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                Debug.Log(gatherer);

                gatherer.TargetDeposit = target.Value.GetComponent<Construction.ResourceDeposit>();

                if (gatherer.TargetDeposit == null) return TaskStatus.Failure;

                targetPosition.Value = target.Value.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}