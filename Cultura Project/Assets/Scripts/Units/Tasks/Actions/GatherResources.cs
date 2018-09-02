using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Cultura.Construction;
using Cultura.Units.Modules;

namespace Cultura.Units.Tasks
{
    public class GatherResources : VillagerAction
    {
        private GathererModule gatherer;

        public ResourceDeposit ResourceDeposit { get; set; }

        public override void OnStart()
        {
            gatherer = baseModule.GetModule<GathererModule>(ModuleID.Gatherer);

            if (gatherer != null)
            {
                ResourceDeposit = gatherer.TargetDeposit;
                abortTrigger.Value = !gatherer.TryStartProcess();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (abortTrigger.Value)
            {
                abortTrigger.Value = false;
                gatherer.StopProcess();
                return TaskStatus.Failure;
            }

            return !gatherer.Gathering ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}