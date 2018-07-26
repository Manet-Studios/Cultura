using Cultura.Construction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Core
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField]
        private PoolableObjectRegistry buildingRegistry;

        private VillageManager villageManager;
        private SelectionManager selectionManager;
        private bool buildMode;

        [SerializeField]
        private int selectedBuilding = 0;

        [SerializeField]
        private Transform blueprintTransform;

        private Camera mainCam;

        // Use this for initialization
        private void Start()
        {
            mainCam = Camera.main;
            villageManager = VillageManager.Instance;
            selectionManager = SelectionManager.Instance;
        }

        // Update is called once per frame
        private void Update()
        {
            #region Hotkeys

            if (Input.GetKeyDown(KeyCode.B))
            {
                buildMode = !buildMode;
                if (buildMode) StartBuildMode();
                else StopBuildMode();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                buildMode = false;
                selectionManager.CancelSelection();

                selectionManager.StartTargetedSelection(SelectionMode.Building, OnSelectBuildingToDemolish, OnCancelDemolish);
            }

            #endregion Hotkeys

            if (buildMode)
            {
                blueprintTransform.position = mainCam.WorldToScreenPoint(Input.mousePosition) + (Vector3.forward * 10);
                blueprintTransform.position = new Vector3((int)blueprintTransform.position.x, (int)blueprintTransform.position.y, 0);
            }
        }

        private void StartBuildMode()
        {
            selectionManager.StartTargetedPositionSelection(OnFindBuildPosition, OnCancelBuilding);
            blueprintTransform.gameObject.SetActive(true);
        }

        private void StopBuildMode()
        {
            selectionManager.CancelSelection();
            blueprintTransform.gameObject.SetActive(false);
        }

        private void OnCancelDemolish()
        {
        }

        private void OnSelectBuildingToDemolish(Transform obj)
        {
            obj.GetComponent<Construction.IBuilding>().OnDemolish();
            Destroy(obj.gameObject);
        }

        private void OnCancelBuilding()
        {
            StopBuildMode();

            buildMode = false;
        }

        private void OnFindBuildPosition(Vector2 pos)
        {
            StopBuildMode();
            IBuilding building = Instantiate(buildingRegistry.buildings[0], new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0), Quaternion.identity).GetComponent<IBuilding>();
            building.OnBuild();
            buildMode = false;
        }
    }
}