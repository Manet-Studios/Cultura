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
    public class VillagerBase : SerializedMonoBehaviour, ISelectable
    {
        [OdinSerialize]
        private ICommand[] assignableCommands;

        [OdinSerialize]
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

        public bool Selected { get; set; }

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
            if (!Selected) return;

            ICommand command = assignableCommands[commandIndex];

            command.StartCommand();
            Debug.Log("Assigned : " + command.CommandID);
        }

        public void OnSelect()
        {
            Selected = true;
        }

        public void OnDeselect()
        {
            Selected = false;
        }
    }
}