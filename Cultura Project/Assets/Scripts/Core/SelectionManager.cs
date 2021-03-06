﻿using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cultura.Core
{
    public enum SelectionMode { Any, NonTerrain, Unit, Building, Blueprint, Resource, Terrain }

    public class SelectionManager : SerializedMonoBehaviour
    {
        private static SelectionManager instance;

        public Color normalOutlineColor;
        public Color hoveredOutlineColor;
        public Color selectedObjectColor;

        [SerializeField]
        private Dictionary<SelectionMode, LayerMask> selectionMasks = new Dictionary<SelectionMode, LayerMask>();

        private List<LocationSelection> locationSelections = new List<LocationSelection>();

        private List<SelectionInfo<object>> selectionInfos = new List<SelectionInfo<object>>();

        [SerializeField]
        private SelectionMode selectionMode;

        private bool targetingSelection;
        private bool targetingLocation;

        private Vector2 startDragPosition;

        private bool canSelect = true;

        private Collider2D[] overlappedColliders = new Collider2D[10];

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

                if (SelectionModeChangeEventHandler != null) SelectionModeChangeEventHandler(selectionMode);
            }
        }

        public event Action<SelectionMode> SelectionModeChangeEventHandler;

        [HideInInspector]
        public List<ISelectable> currentlySelectedObjects = new List<ISelectable>();

        public event Action SelectObjectEventHandler;

        public event Action DeselectObjectEventHandler;

        private EventSystem eventSystem;
        private GraphicRaycaster raycaster;

        private void Start()
        {
            canSelect = true;
            eventSystem = FindObjectOfType<EventSystem>();
            raycaster = FindObjectOfType<GraphicRaycaster>();
        }

        private void Update()
        {
            if (!canSelect) return;

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            raycaster.Raycast(pointerEventData, raycastResults);

            if (raycastResults.Count > 0) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                startDragPosition = cursorPosition;
                OnClick(cursorPosition);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                OnDrag(cursorPosition);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                startDragPosition = Vector2.zero;
                OnRelease(cursorPosition);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResetCallbacks();
                targetingSelection = false;
            }
        }

        public void StartTargetedSelection<T>(SelectionMode target, SelectionInfo<T> selectionInfo)
        {
            ResetCallbacks();

            targetingSelection = true;
            SelectionMode = target;

            SelectionInfo<object> genericInfo = new SelectionInfo<object>((t) => { selectionInfo.onSelectCallback((T)t); },
                selectionInfo.onCancelCallback,
                selectionInfo.selectionCriteria,
                (t) => { return selectionInfo.selectionTransformer((T)t); });

            selectionInfos.Add(genericInfo);

            if (!canSelect) ResetCallbacks();
        }

        public void StartLocationSelection(LocationSelection locationSelection)
        {
            ResetCallbacks();

            targetingLocation = true;
            SelectionMode = SelectionMode.Terrain;

            locationSelections.Add(locationSelection);

            if (!canSelect) ResetCallbacks();
        }

        private void OnClick(Vector2 cursorPosition)
        {
            Physics2D.OverlapPointNonAlloc(cursorPosition, overlappedColliders, selectionMasks[selectionMode]);

            if (targetingLocation)
            {
                if (overlappedColliders[0] != null)
                {
                    for (int i = 0; i < locationSelections.Count; i++)
                    {
                        locationSelections[i].onSelectCallback(cursorPosition);
                    }
                    locationSelections.Clear();

                    ResetCallbacks();
                }
            }

            if (targetingSelection)
            {
                if (overlappedColliders[0] != null)
                {
                    if (selectionInfos[0].selectionCriteria(overlappedColliders[0].transform))
                    {
                        CompleteTargetedSelection(overlappedColliders[0].transform);
                    }
                }
            }
            else
            {
                if (overlappedColliders[0] != null)
                {
                    ISelectable[] selectables = overlappedColliders[0].GetComponents<ISelectable>();

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        for (int i = 0; i < selectables.Length; i++)
                        {
                            if (!currentlySelectedObjects.Contains(selectables[i]))
                            {
                                selectables[i].OnSelect();

                                currentlySelectedObjects.Add(selectables[i]);
                            }
                        }
                        if (SelectObjectEventHandler != null) SelectObjectEventHandler();
                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        for (int i = 0; i < selectables.Length; i++)
                        {
                            if (currentlySelectedObjects.Contains(selectables[i]))
                            {
                                selectables[i].OnDeselect();
                                currentlySelectedObjects.Remove(selectables[i]);
                            }
                        }
                        if (DeselectObjectEventHandler != null) DeselectObjectEventHandler();
                    }
                    else
                    {
                        for (int i = 0; i < currentlySelectedObjects.Count; i++)
                        {
                            currentlySelectedObjects[i].OnDeselect();
                        }
                        currentlySelectedObjects.Clear();
                        if (DeselectObjectEventHandler != null) DeselectObjectEventHandler();

                        for (int i = 0; i < selectables.Length; i++)
                        {
                            selectables[i].OnSelect();
                            currentlySelectedObjects.Add(selectables[i]);
                        }

                        if (SelectObjectEventHandler != null) SelectObjectEventHandler();
                    }
                }
                else
                {
                    for (int i = 0; i < currentlySelectedObjects.Count; i++)
                    {
                        currentlySelectedObjects[i].OnDeselect();
                    }
                    currentlySelectedObjects.Clear();
                    if (DeselectObjectEventHandler != null) DeselectObjectEventHandler();
                }
            }
        }

        private void CompleteTargetedSelection(Transform selectedTransform)
        {
            for (int i = 0; i < selectionInfos.Count; i++)
            {
                selectionInfos[i].onSelectCallback(selectionInfos[i].selectionTransformer(selectedTransform));
            }

            selectionInfos.Clear();
            ResetCallbacks();
        }

        private void OnDrag(Vector2 cursorPosition)
        {
        }

        private void OnRelease(Vector2 cursorPosition)
        {
        }

        private void ResetCallbacks()
        {
            targetingSelection = false;
            targetingLocation = false;

            foreach (SelectionInfo<object> selectionInfo in selectionInfos)
            {
                selectionInfo.onCancelCallback();
            }
            selectionInfos.Clear();

            foreach (LocationSelection locationSelection in locationSelections)
            {
                locationSelection.onCancelCallback();
            }
            locationSelections.Clear();

            SelectionMode = SelectionMode.NonTerrain;
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

        public void CancelSelection()
        {
            ResetCallbacks();
        }

        public struct SelectionInfo<T>
        {
            public Action<T> onSelectCallback;
            public Func<T, Transform> selectionTransformer;
            public Action onCancelCallback;
            public Predicate<Transform> selectionCriteria;

            public SelectionInfo(Action<T> onSelectCallback, Action onCancelCallback, Predicate<Transform> selectionCriteria, Func<T, Transform> selectionTransformer)
            {
                this.onSelectCallback = onSelectCallback;
                this.selectionTransformer = selectionTransformer;
                this.onCancelCallback = onCancelCallback;
                this.selectionCriteria = selectionCriteria;
            }
        }

        public struct LocationSelection
        {
            public Action<Vector2> onSelectCallback;
            public Action onCancelCallback;

            public LocationSelection(Action<Vector2> onSelectCallback, Action onCancelCallback)
            {
                this.onSelectCallback = onSelectCallback;
                this.onCancelCallback = onCancelCallback;
            }
        }
    }
}