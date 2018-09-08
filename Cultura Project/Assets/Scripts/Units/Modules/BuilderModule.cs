using Cultura.Construction;
using System;
using System.Collections;
using UnityEngine;

namespace Cultura.Units.Modules
{
    public class BuilderModule : MonoBehaviour, IVillagerModule
    {
        public int ID
        {
            get
            {
                return (int)ModuleID.Builder;
            }
        }

        [SerializeField]
        private float buildSpeed;

        [SerializeField]
        private int buildAmount;

        private VillagerBase villagerBase;
        public BuildingBlueprint TargetBlueprint { get; set; }

        private Action onCompleteBuildCallback;

        private Coroutine buildingCoroutine;

        public void Initialize(VillagerBase villagerBase)
        {
            this.villagerBase = villagerBase;
        }

        public bool TryStartProcess(Action onCompleteCallback)
        {
            StopProcess();

            if (TargetBlueprint == null)
            {
                return false;
            }
            onCompleteBuildCallback = onCompleteCallback;
            buildingCoroutine = StartCoroutine(BuildProcess());
            return true;
        }

        public void StopProcess()
        {
            onCompleteBuildCallback = null;
            if (buildingCoroutine != null)
            {
                StopCoroutine(buildingCoroutine);
            }
        }

        private IEnumerator BuildProcess()
        {
            while (TargetBlueprint != null && TargetBlueprint.UnderConstruction)
            {
                yield return new WaitForSeconds(1f / buildSpeed);
                TargetBlueprint.Build(buildAmount);
            }
            onCompleteBuildCallback();
        }
    }
}