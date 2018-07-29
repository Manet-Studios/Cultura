using Cultura.Core;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    public class Shelter : Selectable, IStorageBuilding
    {
        [TabGroup("Unit Production")]
        [SerializeField]
        private int unitCapacity;

        [TabGroup("Construction")]
        [SerializeField]
        private ConstructionCosts constructionCost;

        [TabGroup("Unit Production")]
        [SerializeField]
        private float unitProductionTime;

        [TabGroup("Unit Production")]
        [SerializeField]
        private Transform spawnPoint;

        [TabGroup("Storage")]
        [SerializeField]
        private int additionalWoodStorage;

        [TabGroup("Storage")]
        [SerializeField]
        private int additionalStoneStorage;

        [TabGroup("Storage")]
        [SerializeField]
        private int additionalMetalStorage;

        [TabGroup("Storage")]
        [SerializeField]
        private int additionalFoodStorage;

        [TabGroup("Construction")]
        [SerializeField]
        private BuildingBlueprint blueprintPrefab;

        private Cultura.Core.VillageManager villageManager;

        private Coroutine unitProductionCoroutine;

        #region Properties

        public ConstructionCosts ConstructionCost
        {
            get
            {
                return constructionCost;
            }

            set
            {
                constructionCost = value;
            }
        }

        public GameObject Prefab
        {
            get
            {
                return gameObject;
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
            }
        }

        public float UnitProductionTime
        {
            get
            {
                return unitProductionTime;
            }

            set
            {
                unitProductionTime = value;
            }
        }

        public int AdditionalWoodStorage
        {
            get
            {
                return additionalWoodStorage;
            }

            set
            {
                additionalWoodStorage = value;
            }
        }

        public int AdditionalStoneStorage
        {
            get
            {
                return additionalStoneStorage;
            }

            set
            {
                additionalStoneStorage = value;
            }
        }

        public int AdditionalMetalStorage
        {
            get
            {
                return additionalMetalStorage;
            }

            set
            {
                additionalMetalStorage = value;
            }
        }

        public int AdditionalFoodStorage
        {
            get
            {
                return additionalFoodStorage;
            }

            set
            {
                additionalFoodStorage = value;
            }
        }

        public BuildingBlueprint BlueprintPrefab
        {
            get
            {
                return blueprintPrefab;
            }

            set
            {
                blueprintPrefab = value;
            }
        }

        #endregion Properties

        public virtual void OnBuild()
        {
            villageManager = VillageManager.Instance;
            villageManager.UnitCountEventHandler += OnUnitCountUpdate;
            villageManager.UnitCapacity += UnitCapacity;

            villageManager.AddResourceCapacity(Core.Resource.Food, AdditionalFoodStorage);
            villageManager.AddResourceCapacity(Core.Resource.Stone, AdditionalStoneStorage);
            villageManager.AddResourceCapacity(Core.Resource.Metal, AdditionalMetalStorage);
            villageManager.AddResourceCapacity(Core.Resource.Wood, AdditionalWoodStorage);
        }

        protected virtual void OnUnitCountUpdate(bool insufficientUnitCount)
        {
            if (insufficientUnitCount)
            {
                unitProductionCoroutine = StartCoroutine(UnitProductionCoroutine());
            }
            else
            {
                if (unitProductionCoroutine != null) StopCoroutine(unitProductionCoroutine);
            }
        }

        public virtual void OnDemolish()
        {
            villageManager.UnitCountEventHandler -= OnUnitCountUpdate;
            villageManager.UnitCapacity -= UnitCapacity;

            villageManager.RemoveResourceCapacity(Core.Resource.Food, AdditionalFoodStorage);
            villageManager.RemoveResourceCapacity(Core.Resource.Stone, AdditionalStoneStorage);
            villageManager.RemoveResourceCapacity(Core.Resource.Metal, AdditionalMetalStorage);
            villageManager.RemoveResourceCapacity(Core.Resource.Wood, AdditionalWoodStorage);
        }

        private IEnumerator UnitProductionCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnitProductionTime);
            }
        }

        protected virtual void SpawnVillager()
        {
        }
    }
}