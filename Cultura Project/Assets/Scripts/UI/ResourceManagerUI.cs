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

        private Core.ResourceManager resourceManager;

        // Use this for initialization
        private void Start()
        {
            resourceManager = FindObjectOfType<Core.ResourceManager>();
            resourceManager.UpdateWoodSupplyLevelEventHandler += OnWoodSupplyUpdate;
            resourceManager.UpdateStoneSupplyLevelEventHandler += OnStoneSupplyUpdate;
            resourceManager.UpdateMetalSupplyLevelEventHandler += OnMetalSupplyUpdate;
        }

        private void OnMetalSupplyUpdate(int metalSupplyLevel)
        {
            metalText.text = metalSupplyLevel.ToString();
        }

        private void OnStoneSupplyUpdate(int stoneSupplyLevel)
        {
            stoneText.text = stoneSupplyLevel.ToString();
        }

        private void OnWoodSupplyUpdate(int woodSupplyLevel)
        {
            woodText.text = woodSupplyLevel.ToString();
        }
    }
}