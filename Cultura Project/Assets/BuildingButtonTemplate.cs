using Cultura.Construction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cultura.UI
{
    public class BuildingButtonTemplate : MonoBehaviour
    {
        [SerializeField]
        private GameObject tooltipObject;

        [SerializeField]
        private TextMeshProUGUI buildingNameText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private TextMeshProUGUI costText;

        private BuildMenuUI menuUI;
        private int buildingID;

        private Button button;

        public bool selected;

        public void Initialize(BuildMenuUI menuUI, BuildingBlueprint blueprint, int id)
        {
            selected = false;
            CloseTooltip();
            buildingID = id;
            this.menuUI = menuUI;
            if (button == null)
            {
                button = GetComponent<Button>();
            }

            buildingNameText.text = blueprint.buildingName;
            descriptionText.text = blueprint.description;
            costText.text = blueprint.costString;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                menuUI.SelectBuildingToBuild(this, buildingID);
            });
        }

        public void CloseTooltip()
        {
            if (!selected)
            {
                tooltipObject.SetActive(false);
            }
        }

        public void OpenTooltip()
        {
            tooltipObject.SetActive(true);
        }
    }
}