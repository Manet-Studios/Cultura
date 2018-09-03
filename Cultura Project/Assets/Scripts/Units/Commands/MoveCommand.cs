using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cultura.Core;
using UnityEngine;

namespace Cultura.Units.Commands
{
    public struct MoveCommand : IPositionCommand
    {
        private BehaviorTree tree;

        public string CommandID
        {
            get
            {
                return "Move";
            }
        }

        public CommandType Type
        {
            get
            {
                return CommandType.Position;
            }
        }

        public BehaviorTree BehaviorTree
        {
            set
            {
                tree = value;
            }
        }

        public void OnCancelCommand()
        {
            Debug.Log("Move Command Cancelled");
        }

        public void StartCommand()
        {
            SelectionManager.Instance.StartLocationSelection(new SelectionManager.LocationSelection(OnRecieveInformation, OnCancelCommand));
        }

        public void OnRecieveInformation(Vector2 pos)
        {
            SelectionManager.Instance.StartCoroutine(FrameDelay(pos));
        }

        private IEnumerator FrameDelay(Vector2 pos)
        {
            tree.SetVariableValue("Abort Trigger", true);
            yield return null;
            tree.SendEvent<object>(CommandID, pos);
        }
    }
}