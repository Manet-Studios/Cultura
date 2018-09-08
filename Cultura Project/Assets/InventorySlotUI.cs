using Cultura.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cultura.UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector]
        public Inventory.StorageUnit linkedStorageUnit;

        private Registry registry;

        [SerializeField]
        private Image thumbnailImage;

        [SerializeField]
        private TextMeshProUGUI amountText;

        [SerializeField]
        private TextMeshProUGUI nameTooltipText;

        [SerializeField]
        private GameObject tooltipObject;

        [SerializeField]
        private float tooltipAppearanceDelay = 0.25f;

        private Coroutine tooltipAppearanceCoroutine;

        private void Awake()
        {
            registry = VillageManager.RegistryInstance;
        }

        private void Start()
        {
            linkedStorageUnit = null;
        }

        private void OnEnable()
        {
            if (linkedStorageUnit == null || linkedStorageUnit.Quantity == 0)
            {
                thumbnailImage.color = Color.clear;
            }
        }

        public void Initialize(Inventory.StorageUnit storageUnit)
        {
            Debug.Log("Initialized");

            linkedStorageUnit = storageUnit;
            thumbnailImage.color = Color.white;

            if (linkedStorageUnit == null)
            {
                thumbnailImage.color = Color.clear;
            }

            linkedStorageUnit.OnQuantityUpdate += OnSlotQuantityUpdate;

            Item storedItem = registry.ItemRegistry[linkedStorageUnit.StoredItemID];

            nameTooltipText.text = storedItem.name;
            thumbnailImage.sprite = storedItem.icon;
            amountText.text = linkedStorageUnit.Quantity.ToString();

            thumbnailImage.color = Color.white;

            amountText.color = Color.white;
        }

        public void Clear()
        {
            linkedStorageUnit.OnQuantityUpdate -= OnSlotQuantityUpdate;

            linkedStorageUnit = null;
            thumbnailImage.color = Color.clear;
            amountText.color = Color.clear;
            nameTooltipText.text = "Empty";
            tooltipObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (linkedStorageUnit == null)
            {
                return;
            }

            if (tooltipAppearanceCoroutine != null)
            {
                StopCoroutine(tooltipAppearanceCoroutine);
            }

            tooltipAppearanceCoroutine = StartCoroutine(TooltipAppearanceDelay());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tooltipAppearanceCoroutine != null)
            {
                StopCoroutine(tooltipAppearanceCoroutine);
            }

            tooltipObject.SetActive(false);
        }

        private IEnumerator TooltipAppearanceDelay()
        {
            yield return new WaitForSeconds(tooltipAppearanceDelay);
            tooltipObject.SetActive(true);
        }

        private void OnSlotQuantityUpdate(int quantity)
        {
            amountText.text = quantity.ToString();
        }
    }
}