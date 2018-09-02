using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Construction;

namespace Cultura.Units.Tasks
{
    public class DepositResources : VillagerAction
    {
        public SharedTransform targetTransform;

        private InventoryRepository targetRepository;

        public override void OnStart()
        {
            base.OnStart();
            targetRepository = targetTransform.Value.GetComponent<InventoryRepository>();
        }

        public override TaskStatus OnUpdate()
        {
            baseModule.Inventory.TransferContents(targetRepository.Inventory);
            return TaskStatus.Success;
        }
    }
}