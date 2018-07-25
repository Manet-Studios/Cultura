using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
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

        public void OnRecieveInformation(Vector2 pos)
        {
            Debug.Log("Move to " + pos);

            tree.SendEvent<object>(CommandID, pos);
        }
    }
}