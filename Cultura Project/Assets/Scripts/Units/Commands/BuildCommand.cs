using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cultura.Construction;
using Cultura.Core;
using UnityEngine;

namespace Cultura.Units.Commands
{
    public struct BuildCommand : IComponentCommand<Transform>
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
                return CommandType.Component;
            }
        }

        public void OnCancelCommand()
        {
            Debug.Log("Build Command Cancelled");
        }

        public void OnRecieveInformation(Transform obj)
        {
            SelectionManager.Instance.StartCoroutine(FrameDelay(obj));
        }

        public bool SelectionCriteria(Transform transform)
        {
            return transform.GetComponent<BuildingBlueprint>() != null;
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

        private IEnumerator FrameDelay(Transform obj)
        {
            tree.SetVariableValue("Abort Trigger", true);
            yield return null;
            tree.SendEvent<object>(CommandID, obj);
        }
    }
}