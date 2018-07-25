using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Cultura.Construction;

namespace Cultura.Units.Tasks
{
    public class GatherResources : Action
    {
        private IGatherer gatherer;
        public SharedInt resource;

        public ResourceDeposit ResourceDeposit { get; set; }

        public override void OnAwake()
        {
            gatherer = transform.GetComponent<IGatherer>();
        }

        public override void OnStart()
        {
            resource.Value = (int)gatherer.TargetDeposit.Resource;
            ResourceDeposit = gatherer.TargetDeposit;
        }

        public override TaskStatus OnUpdate()
        {
            if (ResourceDeposit != null)
                gatherer.GatherResources();
            return ResourceDeposit == null || gatherer.Inventory.AtResourceCapacity((Core.Resource)resource.Value) ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}