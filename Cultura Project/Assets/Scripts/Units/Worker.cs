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

        protected override void Start()
        {
            base.Start();
        }

        public void GatherResources()
        {
            TargetDeposit.Collect(Inventory);
        }

        public void DepositResources()
        {
            Inventory.TransferContentsToInventory(TargetRepository);
        }
    }
}