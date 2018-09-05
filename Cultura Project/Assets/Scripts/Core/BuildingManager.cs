using Cultura.Construction;
using UnityEngine;

namespace Cultura.Core
{
    public class BuildingManager : MonoBehaviour
    {
        private Registry registry;

        private VillageManager villageManager;
        private SelectionManager selectionManager;

        private bool buildMode;

        [SerializeField]
        private int selectedBuilding = 0;

        [SerializeField]
        private SpriteRenderer blueprintTransform;

        [SerializeField]
        private LayerMask buildingLayerMask;

        [SerializeField]
        private Color placeableBlueprintColor;

        [SerializeField]
        private Color nonplaceableBlueprintColor;

        [SerializeField]
        private Color bluePrintColor;

        private Camera mainCam;
        private Collider2D[] obstructingColliders = new Collider2D[2];

        // Use this for initialization
        private void Start()
        {
            mainCam = Camera.main;
            villageManager = VillageManager.Instance;
            selectionManager = SelectionManager.Instance;
            registry = VillageManager.RegistryInstance;
        }

        // Update is called once per frame
        private void Update()
        {
            #region Hotkeys

            /*if (Input.GetKeyDown(KeyCode.B))
            {
                buildMode = !buildMode;
                if (buildMode)
                {
                    StartBuildMode();
                }
                else
                {
                    StopBuildMode();
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                buildMode = false;
                selectionManager.CancelSelection();

                selectionManager.StartTargetedSelection(SelectionMode.Building,
                    new SelectionManager.SelectionInfo<Transform>(OnSelectBuildingToDemolish,
                    OnCancelDemolish,
                    (obj) => obj.GetComponent<Construction.Modules.BuildingBase>() != null,
                    (t) => { return t; }));
            }
            */

            #endregion Hotkeys

            if (buildMode)
            {
                Vector3 pos = mainCam.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10);
                Vector3 snappedPos = new Vector3((int)pos.x, (int)pos.y, 0);

                blueprintTransform.transform.position = snappedPos;

                int obstructingCount = Physics2D.OverlapBoxNonAlloc(snappedPos, blueprintTransform.bounds.size * .95f, 0, obstructingColliders, buildingLayerMask);

                bool obstruction = obstructingCount > 0;

                blueprintTransform.color = obstruction ? nonplaceableBlueprintColor : placeableBlueprintColor;

                if (!obstruction && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    OnFindBuildPosition(snappedPos);
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StopBuildMode();
                }
            }
        }

        private void StartBuildMode()
        {
            selectionManager.DisableSelection();
            blueprintTransform.gameObject.SetActive(true);
            blueprintTransform.sprite = registry.BuildingRegistry[selectedBuilding].GetComponent<SpriteRenderer>().sprite;
        }

        private void StopBuildMode()
        {
            selectionManager.EnableSelection();

            buildMode = false;
            // if (selectionManager.SelectionMode == SelectionMode.Terrain) selectionManager.CancelSelection();
            blueprintTransform.gameObject.SetActive(false);
        }

        private void OnCancelDemolish()
        {
        }

        private void OnSelectBuildingToDemolish(Transform obj)
        {
            obj.GetComponent<Construction.Modules.BuildingBase>().Demolish();
            Destroy(obj.gameObject);
        }

        private void OnFindBuildPosition(Vector2 pos)
        {
            BuildingBlueprint building = registry.BuildingRegistry[selectedBuilding];
            Instantiate(building, blueprintTransform.transform.position, Quaternion.identity).Initialize();
            StopBuildMode();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(blueprintTransform.transform.position, blueprintTransform.bounds.size);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(blueprintTransform.transform.position, blueprintTransform.bounds.size * .95f);
        }
    }
}