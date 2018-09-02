using Cultura.Core;
using UnityEngine;

namespace Cultura.Units
{
    public interface ICommand
    {
        CommandType Type { get; }
        string CommandID { get; }
        BehaviorDesigner.Runtime.BehaviorTree BehaviorTree { set; }

        void OnCancelCommand();
    }

    public enum CommandType
    { Position, Transform }

    public interface IPositionCommand : ICommand
    {
        void OnRecieveInformation(Vector2 pos);
    }

    public interface ITransformCommand : ICommand
    {
        SelectionMode Target { get; }

        void OnRecieveInformation(Transform obj);
    }
}