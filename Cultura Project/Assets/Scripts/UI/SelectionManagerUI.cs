using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cultura.Core;
using Cultura.Units.Modules;

namespace Cultura.UI
{
    public class SelectionManagerUI : MonoBehaviour
    {
        private SelectionManager selectionManager;

        [SerializeField]
        private SelectionModeTextFormat[] selectionModeTextFormats;

        [SerializeField]
        private TextMeshProUGUI selectionIndicatorText;

        private List<ISelectable> selectedObjects;

        private List<VillagerBase> villagers = new List<VillagerBase>();

        [SerializeField]
        private Button moveCommandButton;

        [SerializeField]
        private Button reassignCommandButton;

        [SerializeField]
        private Button unitCommandButton;

        // Use this for initialization
        private void Start()
        {
            selectionManager = SelectionManager.Instance;
            selectedObjects = selectionManager.currentlySelectedObjects;

            selectionManager.SelectObjectEventHandler += OnSelectObject;
            selectionManager.DeselectObjectEventHandler += OnSelectObject;
            selectionManager.SelectionModeChangeEventHandler += OnSelectionModeChange;
        }

        private void OnSelectionModeChange(SelectionMode obj)
        {
            selectionIndicatorText.color = selectionModeTextFormats[(int)obj].color;
            selectionIndicatorText.text = selectionModeTextFormats[(int)obj].text;
        }

        private void OnSelectObject()
        {
            //Repopulating List
            villagers.Clear();
            for (int i = 0; i < selectedObjects.Count; i++)
            {
                if (selectedObjects[i].GetType() == typeof(VillagerBase))
                {
                    villagers.Add(selectedObjects[i] as VillagerBase);
                }
            }

            moveCommandButton.onClick.RemoveAllListeners();
            reassignCommandButton.onClick.RemoveAllListeners();
            unitCommandButton.onClick.RemoveAllListeners();

            moveCommandButton.onClick.AddListener(() =>
            {
                for (int i = 0; i < villagers.Count; i++)
                {
                    villagers[i].AssignCommand(0);
                }
            });

            reassignCommandButton.onClick.AddListener(() =>
            {
                for (int i = 0; i < villagers.Count; i++)
                {
                    villagers[i].AssignCommand(1);
                }
            });

            unitCommandButton.onClick.AddListener(() =>
            {
                for (int i = 0; i < villagers.Count; i++)
                {
                    villagers[i].AssignCommand(2);
                }
            });
        }

        [System.Serializable]
        public struct SelectionModeTextFormat
        {
            public Color color;
            public string text;
        }
    }
}