using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public enum SelectionMode { Any, Unit, Building, Blueprint, Resource, Terrain }

    public class SelectionManager : MonoBehaviour
    {
        public Color normalOutlineColor;
        public Color hoveredOutlineColor;
        public Color selectedObjectColor;

        public LayerMask selectableLayers;
        public LayerMask unitLayers;
        public LayerMask buildingLayers;
        public LayerMask blueprintLayers;
        public LayerMask resourceLayers;
        public LayerMask terrainLayers;

        [SerializeField]
        private SelectionMode selectionMode;

        private static SelectionManager instance;

        private Action<Transform> onSelectCallback;
        private Action<Vector2> onSelectPositionCallback;
        private Action onCancelCallback;

        private bool targetingSelection;
        private bool selectingLocation = false;

        private bool canSelect = true;

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
                switch (selectionMode)
                {
                    case SelectionMode.Any:
                        selectedMask = selectableLayers;
                        break;

                    case SelectionMode.Unit:
                        selectedMask = unitLayers;

                        break;

                    case SelectionMode.Building:
                        selectedMask = buildingLayers;
                        break;

                    case SelectionMode.Blueprint:
                        selectedMask = blueprintLayers;
                        break;

                    case SelectionMode.Resource:
                        selectedMask = resourceLayers;
                        break;

                    case SelectionMode.Terrain:
                        selectedMask = terrainLayers;
                        break;
                }

                if (SelectionModeChangeEventHandler != null) SelectionModeChangeEventHandler(selectionMode);
            }
        }

        public event Action<SelectionMode> SelectionModeChangeEventHandler;

        private List<ISelectable> currentlySelectedObjects = new List<ISelectable>();
        private LayerMask selectedMask;

        private void Start()
        {
            canSelect = true;
        }

        private void Update()
        {
            if (!canSelect) return;

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!selectingLocation)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Collider2D coll = Physics2D.OverlapPoint(cursorPosition, selectedMask);
                    foreach (ISelectable item in currentlySelectedObjects)
                    {
                        if (item != null)
                            item.OnDeselect();
                    }
                    currentlySelectedObjects.Clear();

                    if (coll != null)
                    {
                        ISelectable selectable = coll.GetComponent<ISelectable>();
                        if (selectable != null && !selectable.Selected)
                        {
                            selectable.OnSelect();
                            currentlySelectedObjects.Add(selectable);
                        }

                        if (targetingSelection)
                        {
                            targetingSelection = false;
                            if (onSelectCallback != null) onSelectCallback(coll.transform);
                            ResetCallbacks();
                        }
                    }
                    else
                    {
                        if (targetingSelection)
                        {
                            CancelSelection();
                        }
                    }
                }
                else if (targetingSelection && Input.GetKeyDown(KeyCode.Escape))
                {
                    CancelSelection();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    selectingLocation = false;
                    if (onSelectPositionCallback != null) onSelectPositionCallback(cursorPosition);
                    ResetCallbacks();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CancelSelection();
                }
            }
        }

        public void CancelSelection()
        {
            SelectionMode = SelectionMode.Any;
            if (onCancelCallback != null) onCancelCallback();
            targetingSelection = false;

            selectingLocation = false;
            ResetCallbacks();
        }

        private void ResetCallbacks()
        {
            SelectionMode = SelectionMode.Any;

            onSelectCallback = null;
            onSelectPositionCallback = null;
            onCancelCallback = null;
        }

        public void StartTargetedSelection(SelectionMode target, Action<Transform> onSelectCallback, Action onCancelCallback)
        {
            targetingSelection = true;
            SelectionMode = target;
            this.onSelectCallback = onSelectCallback;
            this.onCancelCallback = onCancelCallback;

            if (!canSelect)
            {
                CancelSelection();
            }
        }

        public void StartTargetedPositionSelection(Action<Vector2> onSelectPositionCallback, Action onCancelCallback)
        {
            selectingLocation = true;
            SelectionMode = SelectionMode.Terrain;

            this.onSelectPositionCallback = onSelectPositionCallback;
            this.onCancelCallback = onCancelCallback;

            if (!canSelect)
            {
                CancelSelection();
            }
        }

        public void SetSelectionMode(int mode)
        {
            SelectionMode = (SelectionMode)mode;
        }

        public void DisableSelection()
        {
            ResetCallbacks();
            canSelect = false;
        }

        public void EnableSelection()
        {
            ResetCallbacks();
            canSelect = true;
        }
    }
}