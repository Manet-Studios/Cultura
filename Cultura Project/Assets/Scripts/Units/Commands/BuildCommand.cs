using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cultura.Core;
using UnityEngine;

namespace Cultura.Units.Commands
{
    public struct BuildCommand : ITransformCommand
    {
        private BehaviorTree tree;

        public string CommandID
        {
            get
            {
                return "Build";
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
                return SelectionMode.Blueprint;
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
            Debug.Log("Build Command Cancelled");
        }

        public void OnRecieveInformation(Transform obj)
        {
            tree.SendEvent<object, object>(CommandID, obj, (Vector2)obj.position);
        }
    }
}