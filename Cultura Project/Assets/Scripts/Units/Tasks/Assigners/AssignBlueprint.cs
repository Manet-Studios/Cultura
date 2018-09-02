using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units.Tasks
{
    public class AssignBlueprint : Action
    {
        /* private IBuilder builder;

         public SharedTransform target;
         public SharedVector2 targetPosition;

         public override void OnAwake()
         {
             builder = transform.GetComponent<IBuilder>();
         }

         public override TaskStatus OnUpdate()
         {
             if (target.Value != null)
             {
                 builder.Blueprint = target.Value.GetComponent<Construction.BuildingBlueprint>();
                 targetPosition.Value = target.Value.position;
                 return TaskStatus.Success;
             }

             return TaskStatus.Failure;
         }*/
    }
}