using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    public class Shelter : MonoBehaviour, IBuilding
    {
        [SerializeField]
        private int unitCapacity;

        [SerializeField]
        private ConstructionCosts constructionCost;

        [SerializeField]
        private float unitProductionTime;

        private Cultura.Core.VillageManager villageManager;

        private Coroutine unitProductionCoroutine;

        [SerializeField]
        private Transform spawnPoint;

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

        #endregion Properties

        public void OnBuild()
        {
            villageManager = FindObjectOfType<Cultura.Core.VillageManager>();
            villageManager.UnitCountEventHandler += OnUnitCountUpdate;
            villageManager.UnitCapacity += UnitCapacity;
        }

        private void OnUnitCountUpdate(bool insufficientUnitCount)
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

        public void OnDemolish()
        {
            villageManager.UnitCountEventHandler -= OnUnitCountUpdate;
            villageManager.UnitCapacity -= UnitCapacity;
        }

        private IEnumerator UnitProductionCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnitProductionTime);
            }
        }

        private void SpawnVillager()
        {
        }
    }
}