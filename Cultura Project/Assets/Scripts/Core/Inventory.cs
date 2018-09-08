using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    [System.Serializable]
    public class Inventory
    {
        public Dictionary<int, StorageUnit> itemsInInventory = new Dictionary<int, StorageUnit>();

        [SerializeField]
        private int _currentStorage;

        [SerializeField]
        private int _storageLimit;

        public int CurrentStorage
        {
            get
            {
                return _currentStorage;
            }
            set
            {
                _currentStorage = value;

                if (UpdateSupplyLevelEventHandler != null)
                {
                    UpdateSupplyLevelEventHandler(_currentStorage);
                }
            }
        }

        public int StorageLimit
        {
            get
            {
                return _storageLimit;
            }
            set
            {
                _storageLimit = value;
            }
        }

        public bool AtMaxCapacity
        {
            get
            {
                return CurrentStorage >= StorageLimit;
            }
        }

        public event Action<int> UpdateSupplyLevelEventHandler;

        public event Action<StorageUnit> NewItemAddedEventHandler;

        public event Action<StorageUnit> ItemRemovedEventHandler;

        public void TransferContents(Inventory inventory)
        {
            List<int> keys = new List<int>();
            foreach (int key in itemsInInventory.Keys)
            {
                keys.Add(key);
            }

            foreach (int key in keys)
            {
                int excessQuantity = 0;
                inventory.StoreItem(key, RemoveItem(key, itemsInInventory[key].Quantity), out excessQuantity);
                if (excessQuantity > 0)
                {
                    int overflow = 0;
                    StoreItem(key, excessQuantity, out overflow);
                }
            }
        }

        public void StoreItem(int itemID, int quantity, out int excessAmount)
        {
            excessAmount = Mathf.Max(0, (CurrentStorage + quantity) - StorageLimit);
            if (!itemsInInventory.ContainsKey(itemID))
            {
                StorageUnit newUnit = new StorageUnit();
                newUnit.StoredItemID = itemID;

                Debug.Log("Why isn't this working?");
                if (NewItemAddedEventHandler != null)
                {
                    Debug.Log("Why isn't this working2?");

                    NewItemAddedEventHandler(newUnit);
                }

                itemsInInventory.Add(itemID, newUnit);
            }

            int storeableAmount = (quantity - excessAmount);
            itemsInInventory[itemID].Quantity += storeableAmount;
            CurrentStorage += storeableAmount;
        }

        public int RemoveItem(int itemID, int quantity)
        {
            Debug.Log("Item  " + itemID + " had " + quantity + "removed");

            if (quantity < 0)
            {
                throw new Exception("Tried to withdraw negative amount");
            }

            if (!itemsInInventory.ContainsKey(itemID))
            {
                return 0;
            }

            int removableAmount = Mathf.Min(quantity, itemsInInventory[itemID].Quantity);
            itemsInInventory[itemID].Quantity -= removableAmount;
            CurrentStorage -= removableAmount;
            Debug.Log(removableAmount);
            if (itemsInInventory[itemID].Quantity == 0)
            {
                if (ItemRemovedEventHandler != null)
                {
                    ItemRemovedEventHandler(itemsInInventory[itemID]);
                }

                itemsInInventory.Remove(itemID);
            }

            return removableAmount;
        }

        [System.Serializable]
        public class StorageUnit
        {
            [ShowInInspector]
            private int _quantity;

            [ShowInInspector]
            private int _storedItemID;

            public int StoredItemID
            {
                get
                {
                    return _storedItemID;
                }
                set
                {
                    _storedItemID = value;
                }
            }

            public int Quantity
            {
                get
                {
                    return _quantity;
                }
                set
                {
                    _quantity = value;
                    if (OnQuantityUpdate != null)
                    {
                        OnQuantityUpdate(_quantity);
                    }
                }
            }

            public event Action<int> OnQuantityUpdate;
        }
    }
}