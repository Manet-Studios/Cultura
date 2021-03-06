﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cultura.Core
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Selectable : SerializedMonoBehaviour, ISelectable
    {
        public bool Selected { get; set; }

        [SerializeField]
        protected bool outlineable = true;

        protected SpriteRenderer spriteRenderer;
        protected MaterialPropertyBlock propertyBlock;

        protected bool mouseHovering;
        protected Core.SelectionManager selectionManager;
        private int outlineColorId;

        protected virtual void Start()
        {
            outlineColorId = Shader.PropertyToID("_OutlineColor");
            Selected = false;
            selectionManager = Core.SelectionManager.Instance;
            spriteRenderer = GetComponent<SpriteRenderer>();
            propertyBlock = new MaterialPropertyBlock();

            OutlineInitialization();
        }

        private void OutlineInitialization()
        {
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(outlineColorId, selectionManager.normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        private void OnMouseEnter()
        {
            mouseHovering = true;
            if (!outlineable || Selected) return;
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(outlineColorId, selectionManager.hoveredOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        private void OnMouseExit()
        {
            mouseHovering = false;
            if (!outlineable || Selected) return;
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(outlineColorId, selectionManager.normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }

        public void OnSelect()
        {
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(outlineColorId, selectionManager.selectedObjectColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
            Selected = true;
        }

        public void OnDeselect()
        {
            Selected = false;
            if (spriteRenderer == null) return;
            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(outlineColorId, mouseHovering ? selectionManager.hoveredOutlineColor : selectionManager.normalOutlineColor);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}