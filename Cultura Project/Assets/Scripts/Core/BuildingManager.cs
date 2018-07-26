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

        // Use this for initialization
        private void Start()
        {
            villageManager = VillageManager.Instance;
            selectionManager = FindObjectOfType<SelectionManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                buildMode = !buildMode;
                if (buildMode)
                {
                    selectionManager.StartTargetedPositionSelection(OnFindBuildPosition, OnCancelBuilding);
                }
            }
        }

        private void OnCancelBuilding()
        {
            buildMode = false;
        }

        private void OnFindBuildPosition(Vector2 pos)
        {
            Instantiate(buildingRegistry.buildings[0], new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0), Quaternion.identity);
        }
    }
}