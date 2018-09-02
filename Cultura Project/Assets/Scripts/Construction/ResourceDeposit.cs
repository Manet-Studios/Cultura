using Cultura.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    [RequireComponent(typeof(Collider2D))]
    public class ResourceDeposit : Selectable
    {
        [SerializeField]
        public int storedItemID;

        [SerializeField]
        private int quantity;

        [SerializeField]
        private Vector2Int quantityRange;

        protected override void Start()
        {
            base.Start();
            quantity = Random.Range(quantityRange.x, quantityRange.y);
        }

        public void Collect(Inventory inv, int amount)
        {
            int excess = 0;
            inv.StoreItem(storedItemID, Mathf.Min(amount, quantity), out excess);
            excess = Mathf.Max(0, excess);
            quantity -= (amount - excess);

            if (quantity < 1)
            {
                TriggerDestruction();
            }
        }

        private void TriggerDestruction()
        {
            //Stuff
            Destroy(gameObject);
        }
    }
}