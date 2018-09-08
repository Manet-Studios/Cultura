using Cultura.Construction.Modules;
using Cultura.Core;
using System;
using UnityEngine;

namespace Cultura.Construction
{
    public enum BuildingCategory
    { General, Resource, Culture }

    [RequireComponent(typeof(SpriteRenderer))]
    public class BuildingBlueprint : Selectable
    {
        public BuildingBase linkedPrefab;

        [Range(1, 3)]
        public int tier;

        public BuildingCategory type;

        public int unlockPrice;

        [TextArea]
        public string description;

        [SerializeField]
        private int constructionPoints;

        [SerializeField]
        private int maxConstructionPoints;

        public string buildingName;
        public string costString;

        public bool UnderConstruction
        {
            get
            {
                return ConstructionPoints < MaxConstructionPoints;
            }
        }

        public SpriteRenderer Renderer
        {
            get
            {
                return spriteRenderer ?? (spriteRenderer = GetComponent<SpriteRenderer>())
;
            }
        }

        public int ConstructionPoints
        {
            get
            {
                return constructionPoints;
            }

            set
            {
                constructionPoints = value;
                if (OnConstructionPointsUpdate != null)
                {
                    OnConstructionPointsUpdate(constructionPoints);
                }
            }
        }

        public int MaxConstructionPoints
        {
            get
            {
                return maxConstructionPoints;
            }

            set
            {
                maxConstructionPoints = value;
            }
        }

        public event Action<int> OnConstructionPointsUpdate;

        public void Initialize()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Navigation.Grid.Instance.SetPositionsWalkable(transform.position - spriteRenderer.bounds.extents * .95f, transform.position + spriteRenderer.bounds.extents * .95f, false);
        }

        public void Build(int productivity)
        {
            ConstructionPoints += productivity;
            if (ConstructionPoints >= MaxConstructionPoints)
            {
                Instantiate(linkedPrefab, transform.position, Quaternion.identity).Build();
                Navigation.Grid.Instance.SetPositionsWalkable(transform.position - spriteRenderer.bounds.extents * .95f, transform.position + spriteRenderer.bounds.extents * .95f, true);

                Destroy(gameObject);
            }
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}