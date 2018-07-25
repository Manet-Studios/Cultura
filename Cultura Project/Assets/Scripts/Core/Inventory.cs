using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;

namespace Cultura.Core
{
    public enum Resource
    {
        Wood, Stone, Metal, Food
    }

    [System.Serializable]
    public class Inventory
    {
        [OdinSerialize]
        private Dictionary<Resource, StorageUnit> resourceDictionary;

        public event Action<Resource, int> UpdateSupplyLevelEventHandler;

        public bool AtResourceCapacity(Resource resource)
        {
            return resourceDictionary[resource].AtCapacity;
        }

        /// <summary>
        /// Stores resources in inventory
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceAmount"></param>
        /// <returns>Amount of excess resources that can't be deposited into this inventory</returns>
        public int DepositResource(Resource resourceType, int resourceAmount)
        {
            if (resourceAmount < 0)
            {
                Debug.Log(resourceAmount);
                return resourceAmount;
            }
            int spaceRemaining = resourceDictionary[resourceType].limit - resourceDictionary[resourceType].quantity;
            int excess = resourceAmount - spaceRemaining;

            resourceDictionary[resourceType].quantity += Math.Min(spaceRemaining, resourceAmount);

            if (UpdateSupplyLevelEventHandler != null)
                UpdateSupplyLevelEventHandler(resourceType, resourceDictionary[resourceType].quantity);

            return Mathf.Max(0, excess);
        }

        /// <summary>
        /// Stores resources in inventory
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceAmount"></param>
        /// <returns>Amount of resources that can be withdrawn from this inventory</returns>
        public int WithdrawResource(Resource resourceType, int resourceAmount)
        {
            int withDrawAmount = Mathf.Min(resourceDictionary[resourceType].quantity, resourceAmount);

            resourceDictionary[resourceType].quantity -= withDrawAmount;

            if (UpdateSupplyLevelEventHandler != null)
                UpdateSupplyLevelEventHandler(resourceType, resourceDictionary[resourceType].quantity);

            return withDrawAmount;
        }

        public void TransferContentsToInventory(Inventory targetInventory)
        {
            int excessWood = targetInventory.DepositResource(Resource.Wood,
                WithdrawResource(Resource.Wood, resourceDictionary[Resource.Wood].limit));

            DepositResource(Resource.Wood, excessWood);

            int excessStone = targetInventory.DepositResource(Resource.Stone,
                WithdrawResource(Resource.Stone, resourceDictionary[Resource.Stone].limit));

            DepositResource(Resource.Stone, excessStone);

            int excessMetal = targetInventory.DepositResource(Resource.Metal,
                WithdrawResource(Resource.Metal, resourceDictionary[Resource.Metal].limit));

            DepositResource(Resource.Metal, excessMetal);
        }

        public void TransferContentsToInventory(Construction.IRepository targetRepository)
        {
            TransferContentsToInventory(targetRepository.Inventory);
        }

        [System.Serializable]
        public class StorageUnit
        {
            public int quantity;
            public int limit;

            public bool AtCapacity
            {
                get
                {
                    return quantity == limit;
                }
            }
        }
    }
}