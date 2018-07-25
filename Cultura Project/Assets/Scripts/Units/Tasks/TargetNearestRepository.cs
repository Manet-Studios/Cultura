using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using System.Linq;

namespace Cultura.Units.Tasks
{
    public class TargetNearestRepository : Action
    {
        public SharedInt resource;
        public SharedVector2 targetPosition;
        private Construction.ResourceRepository[] resourceRepositories;
        private IDepositor unit;

        public override void OnAwake()
        {
            unit = transform.GetComponent<IDepositor>();
        }

        public override void OnStart()
        {
            resourceRepositories = Object.FindObjectsOfType<Construction.ResourceRepository>()
                .Where(r => !r.Inventory.AtResourceCapacity((Core.Resource)resource.Value))
                .OrderBy(r => Vector2.Distance(r.transform.position, transform.position)).ToArray();
        }

        public override TaskStatus OnUpdate()
        {
            if (resourceRepositories.Length > 0)
            {
                unit.TargetRepository = resourceRepositories[0];
                targetPosition.Value = resourceRepositories[0].transform.position;
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}