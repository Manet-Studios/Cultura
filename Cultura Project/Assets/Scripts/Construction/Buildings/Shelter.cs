using Cultura.Core;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Cultura.Construction.Modules
{
    public class Shelter : MonoBehaviour, IBuildingModule
    {
        public const int id = 0;
        public int ID { get { return id; } }

        [TabGroup("Unit Production")]
        [SerializeField]
        private int unitCapacity;

        [TabGroup("Unit Production")]
        [SerializeField]
        private float unitProductionTime;

        [TabGroup("Unit Production")]
        [SerializeField]
        private Transform spawnPoint;

        private Cultura.Core.VillageManager villageManager;

        private Coroutine unitProductionCoroutine;

        #region Properties

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

        public virtual void OnBuild(BuildingBase buildingBase)
        {
            villageManager = VillageManager.Instance;
            villageManager.UnitCountEventHandler += OnUnitCountUpdate;
            villageManager.UnitCapacity += UnitCapacity;
        }

        protected virtual void OnUnitCountUpdate(bool insufficientUnitCount)
        {
            if (insufficientUnitCount)
            {
                unitProductionCoroutine = StartCoroutine(UnitProductionCoroutine());
            }
            else
            {
                if (unitProductionCoroutine != null)
                {
                    StopCoroutine(unitProductionCoroutine);
                }
            }
        }

        public virtual void OnDemolish()
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

        protected virtual void SpawnVillager()
        {
        }
    }
}