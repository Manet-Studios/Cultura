using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Units
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BasicVillager : MonoBehaviour, IVillager
    {
        [SerializeField]
        private int currentHealth;

        [SerializeField]
        private int maximumHealth;

        [SerializeField]
        private float currentProductivity;

        [SerializeField]
        private float maximumProductivity;

        [SerializeField]
        private int currentHungerLevel;

        [SerializeField]
        private int maximumHungerLevel;

        [SerializeField]
        private float currentHappinessLevel;

        [SerializeField]
        private float maximumHappinessLevel;

        [SerializeField]
        private float currentEnergyLevel;

        [SerializeField]
        private float maximumEnergyLevel;

        [SerializeField]
        private Color normalOutlineColor;

        [SerializeField]
        private Color hoveredOutlineColor;

        private SpriteRenderer spriteRenderer;
        private MaterialPropertyBlock propertyBlock;

        private Core.SelectionManager selectionManager;

        private bool mouseHovering;

        #region Properties

        #region Health

        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }

            set
            {
                currentHealth = value;
            }
        }

        public int MaximumHealth
        {
            get
            {
                return maximumHealth;
            }

            set
            {
                maximumHealth = value;
            }
        }

        #endregion Health

        #region Productivity

        public float CurrentProductivity
        {
            get
            {
                return currentProductivity;
            }

            set
            {
                currentProductivity = value;
            }
        }

        public float MaximumProductivity
        {
            get
            {
                return maximumProductivity;
            }

            set
            {
                maximumProductivity = value;
            }
        }

        #endregion Productivity

        #region Energy

        public float CurrentEnergyLevel
        {
            get
            {
                return currentEnergyLevel;
            }

            set
            {
                currentEnergyLevel = value;
            }
        }

        public float MaximumEnergyLevel
        {
            get
            {
                return maximumEnergyLevel;
            }

            set
            {
                maximumEnergyLevel = value;
            }
        }

        #endregion Energy

        #region Happiness

        public float CurrentHappinessLevel
        {
            get
            {
                return currentHappinessLevel;
            }

            set
            {
                currentHappinessLevel = value;
            }
        }

        public float MaximumHappinessLevel
        {
            get
            {
                return maximumHappinessLevel;
            }

            set
            {
                maximumHappinessLevel = value;
            }
        }

        #endregion Happiness

        #region Hunger

        public int CurrentHungerLevel
        {
            get
            {
                return currentHungerLevel;
            }

            set
            {
                currentHungerLevel = value;
            }
        }

        public int MaximumHungerLevel
        {
            get
            {
                return maximumHungerLevel;
            }

            set
            {
                maximumHungerLevel = value;
            }
        }

        #endregion Hunger

        public bool Selected { get; set; }

        #endregion Properties

        private void Start()
        {
            Selected = false;

            selectionManager = Core.SelectionManager.Instance;

            spriteRenderer = GetComponent<SpriteRenderer>();

            propertyBlock = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor("_Color", normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        private void OnMouseEnter()
        {
            print("War");
            mouseHovering = true;
            if (Selected) return;
            print("War");

            propertyBlock.SetColor("_Color", hoveredOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        private void OnMouseExit()
        {
            print("Wa3r");

            mouseHovering = false;
            if (Selected) return;
            propertyBlock.SetColor("_Color", normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        public void OnDie()
        {
        }

        public void OnSelect()
        {
            propertyBlock.SetColor("_Color", selectionManager.selectedObjectColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
            Selected = true;
        }

        public void OnDeselect()
        {
            Selected = false;

            propertyBlock.SetColor("_Color", mouseHovering ? hoveredOutlineColor : normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        public void OnSpawn()
        {
        }
    }
}