using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cultura.UI
{
    public class ResourceManagerUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI woodText;

        [SerializeField]
        private TextMeshProUGUI stoneText;

        [SerializeField]
        private TextMeshProUGUI metalText;

        private Core.VillageManager villageManager;

        private void Start()
        {
            villageManager = FindObjectOfType<Core.VillageManager>();
            villageManager.inventory.UpdateSupplyLevelEventHandler += OnSupplyUpdate;
        }

        private void OnSupplyUpdate(Core.Resource resource, int resourceAmount)
        {
            switch (resource)
            {
                case Core.Resource.Wood:
                    woodText.text = resourceAmount.ToString();

                    break;

                case Core.Resource.Stone:

                    stoneText.text = resourceAmount.ToString();
                    break;

                case Core.Resource.Metal:
                    metalText.text = resourceAmount.ToString();

                    break;
            }
        }
    }
}