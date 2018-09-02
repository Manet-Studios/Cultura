using Cultura.Construction;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Cultura.Units.Modules
{
    public class GathererModule : MonoBehaviour, IVillagerModule
    {
        public int ID
        {
            get
            {
                return (int)ModuleID.Gatherer;
            }
        }

        public ResourceDeposit TargetDeposit { get; set; }

        [SerializeField]
        private int[] gatherableItemIds;

        /// <summary>
        /// How many times a resource can be gathered in one second by this gatherer
        /// </summary>
        public float baseGatherSpeed;

        /// <summary>
        /// How much a resource can be gathered in one gathering
        /// </summary> [SerializeField]
        public int baseGatherAmount;

        private VillagerBase villagerBase;

        private Coroutine gatheringCoroutine;

        public bool Gathering { get; set; }

        public void Initialize(VillagerBase villagerBase)
        {
            this.villagerBase = villagerBase;
            Gathering = false;
        }

        public bool TryStartProcess()
        {
            Gathering = false;

            if (gatheringCoroutine != null)
                StopCoroutine(gatheringCoroutine);

            if (TargetDeposit == null || !gatherableItemIds.Any(id => id == TargetDeposit.storedItemID))
                return false;

            gatheringCoroutine = StartCoroutine(GatherProcess());
            return true;
        }

        public void StopProcess()
        {
            if (gatheringCoroutine != null) StopCoroutine(gatheringCoroutine);
            Gathering = false;
        }

        private IEnumerator GatherProcess()
        {
            Gathering = true;

            while (TargetDeposit != null && !villagerBase.Inventory.AtMaxCapacity)
            {
                yield return new WaitForSeconds(1f / baseGatherSpeed);
                TargetDeposit.Collect(villagerBase.Inventory, baseGatherAmount);
            }

            Gathering = false;
        }
    }
}