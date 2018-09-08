using Cultura.Core;
using System.Linq;
using TMPro;
using UnityEngine;

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

            gameObject.SetActive(false);
        }

        private void OnItemRemoved(Inventory.StorageUnit obj)
        {
            inventorySlotUIs.First(slot => slot.linkedStorageUnit == obj).Clear();
        }

        private void OnItemAdded(Inventory.StorageUnit obj)
        {
            Debug.Log(obj.StoredItemID + " : " + obj.Quantity + " added");
            InventorySlotUI slotUI = inventorySlotUIs.FirstOrDefault(slot => slot.linkedStorageUnit == null);
            if (slotUI != null)
            {
                Debug.Log("Initialized SLot");
                slotUI.Initialize(obj);
            }
        }

        private void OnSupplyUpdate(int storageAmount)
        {
            Debug.Log("Supply Update");

            storageQuantityText.text = storageAmount + "/" + villageManager.inventory.StorageLimit;
        }
    }
}