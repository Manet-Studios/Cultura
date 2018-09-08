using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class VillageManager : SerializedMonoBehaviour
    {
        public static VillageManager Instance;

        public static Registry RegistryInstance;

        [SerializeField]
        private Registry registry;

        [NonSerialized]
        [OdinSerialize]
        public Inventory inventory;

        [SerializeField]
        private int baseInventoryAmount;

        [SerializeField]
        private int unitCount;

        [SerializeField]
        private int unitCapacity;

        public int CurrentCulturePoints { get; set; }

        public List<int> unlockedBuildings = new List<int>();

        private bool requireAdditionalUnits = false;

        public int UnitCount
        {
            get
            {
                return unitCount;
            }

            set
            {
                unitCount = Mathf.Min(value, UnitCapacity);

                if (UnitCountEventHandler != null)
                {
                    if (requireAdditionalUnits != unitCount < UnitCapacity)
                    {
                        requireAdditionalUnits = unitCount < UnitCapacity;
                        UnitCountEventHandler(requireAdditionalUnits);
                    }
                }
            }
        }

        public int UnitCapacity
        {
            get
            {
                return unitCapacity;
            }

            set
            {
                unitCapacity = value;

                if (UnitCountEventHandler != null)
                {
                    if (requireAdditionalUnits != UnitCount < UnitCapacity)
                    {
                        requireAdditionalUnits = UnitCount < UnitCapacity;
                        UnitCountEventHandler(requireAdditionalUnits);
                    }
                }
            }
        }

        public event Action<bool> UnitCountEventHandler;

        public event Action<int> UnlockBuildingEventHandler;

        private void Awake()
        {
            Instance = this;
            RegistryInstance = registry;
        }

        public void ChangeStorageCapacity(int amount)
        {
            inventory.StorageLimit += amount;
            inventory.StorageLimit = Mathf.Max(baseInventoryAmount);
        }

        public bool CanUnlockBuilding(int buildingID)
        {
            return !unlockedBuildings.Contains(buildingID) && CurrentCulturePoints >= RegistryInstance.BuildingRegistry[buildingID].unlockPrice;
        }

        public void UnlockBuilding(int buildingID)
        {
            if (CanUnlockBuilding(buildingID))
            {
                CurrentCulturePoints -= RegistryInstance.BuildingRegistry[buildingID].unlockPrice;
                unlockedBuildings.Add(buildingID);
            }
        }
    }
}