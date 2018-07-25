using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cultura.Core;
using UnityEngine;

namespace Cultura.Units.Commands
{
    public struct GatherCommand : ITransformCommand
    {
        private BehaviorTree tree;

        public string CommandID
        {
            get
            {
                return "Gather";
            }
        }

        public BehaviorTree BehaviorTree
        {
            set
            {
                tree = value;
            }
        }

        public SelectionMode Target
        {
            get
            {
                return SelectionMode.Resource;
            }
        }

        public CommandType Type
        {
            get
            {
                return CommandType.Transform;
            }
        }

        public void OnCancelCommand()
        {
            Debug.Log("Gather Command Cancelled");
        }

        public void OnRecieveInformation(Transform obj)
        {
            tree.SendEvent<object, object>(CommandID, obj, (Vector2)obj.position);
        }
    }
}