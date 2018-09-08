using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cultura.Units.Modules;

namespace Cultura.Units.Tasks
{
    public class AssignBlueprint : VillagerAction
    {
        private BuilderModule builder;

        public SharedTransform target;
        public SharedVector2 targetPosition;

        public override void OnStart()
        {
            builder = baseModule.GetModule<BuilderModule>(ModuleID.Builder);
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value != null)
            {
                builder.TargetBlueprint = target.Value.GetComponent<Construction.BuildingBlueprint>();
                targetPosition.Value = target.Value.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}