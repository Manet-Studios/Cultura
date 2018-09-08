using Cultura.Construction;
using Cultura.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Cultura.UI
{
    public class BuildMenuUI : MonoBehaviour
    {
        private Registry registry;
        private List<int> unlockedBuildings;

        [SerializeField]
        private BuildingButtonTemplate template;

        [SerializeField]
        private Button buildButton;

        [SerializeField]
        private BuildingManager buildingManager;

        [SerializeField]
        private RectTransform[] panels = new RectTransform[3];

        [SerializeField]
        private Vector2 padding;

        [SerializeField]
        private Vector2 spacing;

        private BuildingCategory selectedCategory;

        private BuildingButtonTemplate selectedTemplate;

        private int selectedID;

        // Use this for initialization
        private void Start()
        {
            registry = VillageManager.RegistryInstance;
            unlockedBuildings = VillageManager.Instance.unlockedBuildings;
        }

        public void InitializePanels(int category)
        {
            buildButton.onClick.RemoveAllListeners();

            selectedID = -1;
            selectedCategory = (BuildingCategory)category;

            List<BlueprintIdPair> buildingBlueprints = new List<BlueprintIdPair>();

            for (int i = 0; i < unlockedBuildings.Count; i++)
            {
                if (registry.BuildingRegistry[unlockedBuildings[i]].type == selectedCategory)
                {
                    buildingBlueprints.Add(new BlueprintIdPair(unlockedBuildings[i], registry.BuildingRegistry[unlockedBuildings[i]]));
                }
            }

            if (buildingBlueprints.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                BlueprintIdPair[] tieredBlueprints = buildingBlueprints.Where(b => b.blueprint.tier == i + 1).ToArray();
                RectTransform parentPanel = panels[i];

                List<BuildingButtonTemplate> buttonTemplates = new List<BuildingButtonTemplate>();
                buttonTemplates.AddRange(parentPanel.GetComponentsInChildren<BuildingButtonTemplate>());

                int lengthDifference = tieredBlueprints.Length - buttonTemplates.Count;

                for (int diff = lengthDifference; diff < 0; diff++)
                {
                    buttonTemplates[tieredBlueprints.Length - (diff + 1)].gameObject.SetActive(false);
                }

                for (int h = 0; h < lengthDifference; h++)
                {
                    BuildingButtonTemplate newTemplate = Instantiate(template, parentPanel);
                    buttonTemplates.Add(newTemplate);
                }

                for (int j = 0; j < buttonTemplates.Count; j++)
                {
                    buttonTemplates[i].Initialize(this, tieredBlueprints[i].blueprint, tieredBlueprints[i].id);
                }

                SelectBuildingToBuild(-1);
            }
        }

        public void SelectBuildingToBuild(int id)
        {
            selectedID = id;
            buildButton.onClick.RemoveAllListeners();

            if (selectedID == -1)
            {
                buildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                buildButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = new Color32(255, 255, 255, 60);
            }
            else
            {
                buildButton.GetComponent<Image>().color = Color.white;
                buildButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.white;
                buildButton.onClick.AddListener(() =>
                {
                    buildingManager.StartBuildMode(id);
                    CloseMenu();
                });
            }
        }

        public void SelectBuildingToBuild(BuildingButtonTemplate buttonTemplate, int id)
        {
            selectedID = id;
            buildButton.onClick.RemoveAllListeners();

            if (selectedTemplate != null)
            {
                selectedTemplate.selected = false;
                selectedTemplate.CloseTooltip();
            }

            selectedTemplate = buttonTemplate;
            selectedTemplate.selected = true;

            selectedTemplate.OpenTooltip();
            if (selectedID == -1)
            {
                buildButton.GetComponent<Image>().color = new Color32(255, 255, 255, 60);
                buildButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = new Color32(255, 255, 255, 60);
            }
            else
            {
                buildButton.GetComponent<Image>().color = Color.white;
                buildButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.white;
                buildButton.onClick.AddListener(() =>
                {
                    buildingManager.StartBuildMode(id);
                    CloseMenu();
                });
            }
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }

        private struct BlueprintIdPair
        {
            public int id;
            public BuildingBlueprint blueprint;

            public BlueprintIdPair(int id, BuildingBlueprint blueprint)
            {
                this.id = id;
                this.blueprint = blueprint;
            }
        }
    }
}