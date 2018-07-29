using Cultura.Core;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Construction
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BuildingBlueprint : Selectable
    {
        public GameObject linkedPrefab;

        [SerializeField]
        private int constructionPoints;

        private Collider2D coll;

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
                if (OnConstructionPointsUpdate != null) OnConstructionPointsUpdate(constructionPoints);
            }
        }

        public int MaxConstructionPoints { get; set; }

        public event Action<int> OnConstructionPointsUpdate;

        public void Initialize(IBuilding linkedBuilding)
        {
            MaxConstructionPoints = linkedBuilding.ConstructionCost.constructionPoints;
            linkedPrefab = (linkedBuilding as MonoBehaviour).gameObject;
            spriteRenderer = GetComponent<SpriteRenderer>();
            Navigation.Grid.Instance.SetPositionsWalkable(transform.position - spriteRenderer.bounds.extents * .95f, transform.position + spriteRenderer.bounds.extents * .95f, false);
        }

        public void Build(int productivity)
        {
            ConstructionPoints += productivity;
            if (ConstructionPoints > MaxConstructionPoints)
            {
                Instantiate(linkedPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);

                Navigation.Grid.Instance.SetPositionsWalkable(transform.position - spriteRenderer.bounds.extents * .95f, transform.position + spriteRenderer.bounds.extents * .95f, true);
            }
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}