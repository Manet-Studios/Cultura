using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public enum SelectionMode { Any, Unit, Building, Resource, Terrain }

    public class SelectionManager : MonoBehaviour
    {
        public Color normalOutlineColor;
        public Color hoveredOutlineColor;
        public Color selectedObjectColor;

        public LayerMask unitLayers;
        public LayerMask buildingLayers;
        public LayerMask resourceLayers;

        [SerializeField]
        private SelectionMode selectionMode;

        private static SelectionManager instance;

        private Action<Transform> onSelectCallback;
        private Action onCancelCallback;

        private bool targetingSelection;

        public static SelectionManager Instance
        {
            get
            {
                return instance ?? (instance = FindObjectOfType<SelectionManager>());
            }
        }

        public SelectionMode SelectionMode
        {
            get
            {
                return selectionMode;
            }

            set
            {
                selectionMode = value;
            }
        }

        private List<ISelectable> currentlySelectedObjects = new List<ISelectable>();
        private LayerMask selectedMask;
        private Vector2 startDragLocation;

        private void Update()
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
                    if (targetingSelection)
                    {
                        targetingSelection = false;
                        onSelectCallback(coll.transform);
                    }
                }
                else
                {
                    if (targetingSelection)
                    {
                        targetingSelection = false;
                        onCancelCallback();
                        SelectionMode = SelectionMode.Any;
                    }
                }
            }
        }

        public void StartTargetedSelection(SelectionMode target, Action<Transform> onSelectCallback, Action onCancelCallback)
        {
            targetingSelection = true;
            SelectionMode = target;
            this.onSelectCallback = onSelectCallback;
            this.onCancelCallback = onCancelCallback;
        }
    }
}