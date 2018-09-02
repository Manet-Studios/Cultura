using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cultura.Core;
using System;

namespace Cultura.UI
{
    public class InventoryManagerUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI storageQuantityText;

        [SerializeField]
        private InventorySlotUI[] inventorySlotUIs = new InventorySlotUI[28];

        private Core.VillageManager villageManager;

        private void Start()
        {
            villageManager = Core.VillageManager.Instance;
            storageQuantityText.text = 0 + "/" + villageManager.inventory.StorageLimit;

            villageManager.inventory.UpdateSupplyLevelEventHandler += OnSupplyUpdate;
            villageManager.inventory.NewItemAddedEventHandler += OnItemAdded;
            villageManager.inventory.ItemRemovedEventHandler += OnItemRemoved;
        }

        private void OnItemRemoved(Inventory.StorageUnit obj)
        {
            inventorySlotUIs.First(slot => slot.linkedStorageUnit == obj).Clear();
        }

        private void OnItemAdded(Inventory.StorageUnit obj)
        {
            inventorySlotUIs.First(slot => slot.linkedStorageUnit == null).Initialize(obj);
        }

        private void OnSupplyUpdate(int storageAmount)
        {
            storageQuantityText.text = storageAmount + "/" + villageManager.inventory.StorageLimit;
        }
    }
}