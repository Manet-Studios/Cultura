using Cultura.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public enum SelectionMode { Unit, Building }

    public class SelectionManager : MonoBehaviour
    {
        public Color selectedObjectColor;

        public LayerMask unitLayers;
        public LayerMask buildingLayers;

        private SelectionMode selectionMode;

        private static SelectionManager instance;

        public static SelectionManager Instance
        {
            get
            {
                return instance ?? (instance = FindObjectOfType<SelectionManager>());
            }
        }

        private List<ISelectable> currentlySelectedObjects = new List<ISelectable>();

        private Vector2 startDragLocation;

        private void Update()
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LayerMask selectedMask = selectionMode == SelectionMode.Building ? buildingLayers : unitLayers;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                selectionMode = (SelectionMode)(((int)selectionMode + 1) % 2);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider2D coll = Physics2D.OverlapPoint(cursorPosition, selectedMask);
                foreach (ISelectable item in currentlySelectedObjects)
                {
                    item.OnDeselect();
                }
                currentlySelectedObjects.Clear();

                if (coll != null)
                {
                    ISelectable selectable = coll.GetComponent<ISelectable>();
                    selectable.OnSelect();
                    currentlySelectedObjects.Add(selectable);
                }
            }
        }
    }
}