using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using System.Linq;

namespace Cultura.Units.Tasks
{
    public class TargetNearestRepository : VillagerAction
    {
        public SharedTransform targetTransform;
        public SharedVector2 targetPosition;
        private Construction.InventoryRepository[] resourceRepositories;

        public override void OnStart()
        {
            base.OnStart();
            resourceRepositories = Object.FindObjectsOfType<Construction.InventoryRepository>()
                .Where(r => !r.Inventory.AtMaxCapacity)
                .OrderBy(r => Vector2.Distance(r.transform.position, transform.position)).ToArray();
        }

        public override TaskStatus OnUpdate()
        {
            if (resourceRepositories.Length > 0)
            {
                targetTransform.Value = resourceRepositories[0].transform;
                targetPosition.Value = targetTransform.Value.position;
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}