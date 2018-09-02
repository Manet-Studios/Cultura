using Cultura.Core;
using UnityEngine;

namespace Cultura.Units
{
    public interface ICommand
    {
        CommandType Type { get; }
        string CommandID { get; }
        BehaviorDesigner.Runtime.BehaviorTree BehaviorTree { set; }

        void StartCommand();

        void OnCancelCommand();
    }

    public enum CommandType
    { Position, Component }

    public interface IPositionCommand : ICommand
    {
        void OnRecieveInformation(Vector2 pos);
    }

    public interface IComponentCommand<T> : ICommand
    {
        SelectionMode Target { get; }

        void OnRecieveInformation(T obj);

        bool SelectionCriteria(Transform transform);

        T SelectionTransformer(Transform obj);
    }
}