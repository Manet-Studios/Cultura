using Cultura.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Cultura.Construction
{
    public class InventoryRepository : SerializedMonoBehaviour
    {
        public bool depositInPlayerInventory;

        [HideIf("depositInPlayerInventory")]
        [OdinSerialize]
        private Inventory inventoryUsed;

        public Inventory Inventory
        {
            get
            {
                return inventoryUsed;
            }
        }

        private void Start()
        {
            if (depositInPlayerInventory)
            {
                inventoryUsed = VillageManager.Instance.inventory;
            }
        }
    }
}