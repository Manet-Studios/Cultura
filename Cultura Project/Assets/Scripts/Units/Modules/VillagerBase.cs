using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Core;

namespace Cultura.Units.Modules
{
    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(BehaviorDesigner.Runtime.BehaviorTree))]
    public class VillagerBase : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private ICommand[] assignableCommands;

        [SerializeField]
        private Inventory inventory;

        private Dictionary<int, IVillagerModule> modules = new Dictionary<int, IVillagerModule>();

        protected BehaviorDesigner.Runtime.BehaviorTree tree;

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }
        }

        private void Start()
        {
            tree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();

            for (int i = 0; i < assignableCommands.Length; i++)
            {
                assignableCommands[i].BehaviorTree = tree;
            }

            IVillagerModule[] villagerModules = GetComponents<IVillagerModule>();
            foreach (IVillagerModule module in villagerModules)
            {
                if (modules.ContainsKey(module.ID))
                {
                    Debug.Log(gameObject.name + " has an extra module of type " + (ModuleID)module.ID);
                    continue;
                }
                modules.Add(module.ID, module);
                module.Initialize(this);
            }
        }

        public T GetModule<T>(ModuleID moduleID) where T : class, IVillagerModule
        {
            if (modules.ContainsKey((int)moduleID)) return modules[(int)moduleID] as T;

            return null;
        }

        public void AssignCommand(int commandIndex)
        {
            ICommand command = assignableCommands[commandIndex];

            if (command.Type == CommandType.Position)
            {
                IPositionCommand positionCommand = (IPositionCommand)command;
                Debug.Log("Assigned : " + positionCommand.CommandID);

                SelectionManager.Instance.StartTargetedPositionSelection(positionCommand.OnRecieveInformation, positionCommand.OnCancelCommand);
            }
            else if (command.Type == CommandType.Transform)
            {
                ITransformCommand transformCommand = (ITransformCommand)command;
                Debug.Log("Assigned : " + transformCommand.CommandID);

                SelectionManager.Instance.StartTargetedSelection(transformCommand.Target, transformCommand.OnRecieveInformation,
                    transformCommand.OnCancelCommand);
            }
        }
    }
}