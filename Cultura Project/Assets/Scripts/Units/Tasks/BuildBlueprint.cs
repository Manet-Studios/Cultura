using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Cultura.Construction;

namespace Cultura.Units.Tasks
{
    public class BuildBlueprint : Action
    {
        private IBuilder builder;

        public BuildingBlueprint Blueprint { get; set; }

        public override void OnAwake()
        {
            builder = transform.GetComponent<IBuilder>();
        }

        public override void OnStart()
        {
            Blueprint = builder.Blueprint;
        }

        public override TaskStatus OnUpdate()
        {
            if (Blueprint != null)
                builder.Build();
            return Blueprint == null ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}