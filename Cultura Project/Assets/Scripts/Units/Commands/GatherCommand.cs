using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cultura.Construction;
using Cultura.Core;
using UnityEngine;

namespace Cultura.Units.Commands
{
    public struct GatherCommand : IComponentCommand<Transform>
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
                return CommandType.Component;
            }
        }

        public void OnCancelCommand()
        {
            Debug.Log("Gather Command Cancelled");
        }

        public void OnRecieveInformation(Transform obj)
        {
            tree.SetVariableValue("Abort Trigger", true);

            tree.SendEvent<object>(CommandID, obj);
        }

        public bool SelectionCriteria(Transform transform)
        {
            return transform.GetComponent<ResourceDeposit>() != null;
        }

        public Transform SelectionTransformer(Transform obj)
        {
            return obj;
        }

        public void StartCommand()
        {
            SelectionManager.Instance.StartTargetedSelection(Target,
                             new SelectionManager.SelectionInfo<Transform>(
                                 OnRecieveInformation,
                                 OnCancelCommand,
                                 SelectionCriteria,
                                 SelectionTransformer));
        }
    }
}