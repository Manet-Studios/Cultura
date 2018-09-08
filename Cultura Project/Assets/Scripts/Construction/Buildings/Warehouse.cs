using Cultura.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cultura.Construction.Modules
{
    public class Warehouse : SerializedMonoBehaviour, IBuildingModule
    {
        private const int id = 1;

        [SerializeField]
        private int additionalStorage;

        [SerializeField]
        private DemoCondition demoCondition;

        private BuildingBase buildingBase;

        public int ID
        {
            get
            {
                return id;
            }
        }

        public void OnBuild(BuildingBase buildingBase)
        {
            demoCondition.condition = (b) => VillageManager.Instance.inventory.CurrentStorage < VillageManager.Instance.inventory.StorageLimit - additionalStorage;
            this.buildingBase = buildingBase;

            VillageManager.Instance.ChangeStorageCapacity(additionalStorage);
            buildingBase.AddDemolitionCondition(demoCondition);
        }

        public void OnDemolish()
        {
            buildingBase.RemoveDemolitionCondition(demoCondition);
        }
    }
}