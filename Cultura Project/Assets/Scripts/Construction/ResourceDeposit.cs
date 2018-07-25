using Cultura.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    [RequireComponent(typeof(Collider2D))]
    public class ResourceDeposit : Selectable
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

        public void Collect(Inventory inv, int amount)
        {
            Debug.Log("Amount remaining : " + ResourceAmount);
            inv.DepositResource(resource, amount);
            ResourceAmount -= amount;

            if (ResourceAmount < 1)
            {
                TriggerDestruction();
            }
        }

        private void TriggerDestruction()
        {
            Destroy(gameObject);
        }
    }
}