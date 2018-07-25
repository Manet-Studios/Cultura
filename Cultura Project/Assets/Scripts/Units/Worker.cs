using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Core;
using Cultura.Construction;
using Sirenix.Serialization;

namespace Cultura.Units
{
    public class Worker : Unit, IGatherer, IDepositor
    {
        [OdinSerialize]
        private Inventory inventory;

        [SerializeField]
        private ResourceRepository targetRepository;

        [SerializeField]
        private ResourceDeposit targetDeposit;

        public int productivity;

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }

            set
            {
                inventory = value;
            }
        }

        public ResourceRepository TargetRepository
        {
            get
            {
                return targetRepository;
            }

            set
            {
                targetRepository = value;
            }
        }

        public ResourceDeposit TargetDeposit
        {
            get
            {
                return targetDeposit;
            }

            set
            {
                targetDeposit = value;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AssignCommand(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AssignCommand(1);
            }
        }

        public void GatherResources()
        {
            TargetDeposit.Collect(Inventory, productivity);
        }

        public void DepositResources()
        {
            Inventory.TransferContentsToInventory(TargetRepository);
        }
    }
}