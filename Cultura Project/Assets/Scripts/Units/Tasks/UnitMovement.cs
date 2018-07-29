using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Cultura.Units.Tasks
{
    public class UnitMovement : Action
    {
        public SharedVector2 targetPosition;
        private Unit unit;
        private int pathStatus = 0;

        private bool completedPath = false;

        public override void OnAwake()
        {
            unit = transform.GetComponent<Unit>();
        }

        public override void OnStart()
        {
            pathStatus = 0;
            completedPath = false;
            unit.FindPath(targetPosition.Value, OnFindPath, OnCompletePath);
        }

        private void OnCompletePath()
        {
            completedPath = true;
        }

        private void OnFindPath(bool pathFound)
        {
            pathStatus = pathFound ? 1 : -1;
        }

        public override TaskStatus OnUpdate()
        {
            if (pathStatus == 1)
                return completedPath ? TaskStatus.Success : TaskStatus.Running;
            else
                return pathStatus == 0 ? TaskStatus.Running : TaskStatus.Failure;
        }
    }
}