using Cultura.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    [RequireComponent(typeof(Collider2D))]
    public class ResourceDeposit : Selectable, ISelectable
    {
        [SerializeField]
        private Resource resource;

        [SerializeField]
        private int resourceAmount;

        public int ResourceAmount
        {
            get
            {
                return resourceAmount;
            }

            set
            {
                resourceAmount = value;
            }
        }

        public Resource Resource
        {
            get
            {
                return resource;
            }

            set
            {
                resource = value;
            }
        }

        public bool Collect(Inventory inv)
        {
            if (ResourceAmount < 1) return false;

            inv.DepositResource(resource, 1);
            ResourceAmount--;

            if (ResourceAmount < 1)
            {
                TriggerDestruction();
            }

            return true;
        }

        private void TriggerDestruction()
        {
            Destroy(gameObject);
        }
    }
}