using BehaviorDesigner.Runtime.Tasks;
using Cultura.Construction;
using Cultura.Units.Modules;

namespace Cultura.Units.Tasks
{
    public class BuildBlueprint : VillagerAction
    {
        private BuilderModule builder;

        private BuildingBlueprint Blueprint { get; set; }

        private bool complete;

        public override void OnStart()
        {
            builder = baseModule.GetModule<BuilderModule>(ModuleID.Builder);

            if (builder != null)
            {
                Blueprint = builder.TargetBlueprint;
                abortTrigger.Value = !builder.TryStartProcess(OnCompleteCallback);
                complete = false;
            }
        }

        private void OnCompleteCallback()
        {
            complete = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (abortTrigger.Value)
            {
                abortTrigger.Value = false;
                builder.StopProcess();
                return TaskStatus.Failure;
            }

            return complete ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}