using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Cultura.Construction;
using Cultura.Units.Modules;

namespace Cultura.Units.Tasks
{
    public class VillagerAction : Action
    {
        public SharedObject baseModuleObject;

        protected VillagerBase baseModule;

        public SharedBool abortTrigger;

        public override void OnAwake()
        {
            abortTrigger.Value = false;
            baseModule = (VillagerBase)baseModuleObject.Value;
        }
    }
}