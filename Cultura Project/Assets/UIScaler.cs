using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaler : MonoBehaviour
{
    private Vector3 originalSize;

    [SerializeField]
    private float sizeModifier;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = transform as RectTransform;
        originalSize = rectTransform.localScale;
    }

    public void Grow()
    {
        rectTransform.localScale *= sizeModifier;
    }

    public void Shrink()
    {
        rectTransform.localScale = originalSize;
    }
}