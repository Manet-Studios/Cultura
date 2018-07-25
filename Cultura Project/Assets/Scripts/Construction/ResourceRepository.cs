using System.Collections;
using System.Collections.Generic;
using Cultura.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Cultura.Construction
{
    public class ResourceRepository : SerializedMonoBehaviour, IRepository
    {
        public bool depositInPlayerInventory;

        [HideIf("depositInPlayerInventory")]
        [OdinSerialize]
        private Inventory inventoryUsed;

        public Inventory Inventory
        {
            get
            {
                return inventoryUsed;
            }
        }

        private void Start()
        {
            if (depositInPlayerInventory) inventoryUsed = VillageManager.Instance.inventory;
        }
    }
}