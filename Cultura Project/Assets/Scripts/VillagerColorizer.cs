using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VillagerColorizer : MonoBehaviour
{

    Texture2D mColorSwapTex;
    Color[] spriteColors;
    SpriteRenderer mSpriteRenderer;

    public enum HairColor
    {
        Blue,
        Brown,
        Orange,
        Blonde,
        Black
    }

    public enum SkinColor
    {
        Light,
        Medium,
        Dark
    }

    public enum ShirtColor
    {
        Brown,
        Green
    }

    public enum PantsColor
    {
        Light,
        Dark
    }

    [SerializeField] HairColor hair = HairColor.Blue;
    [SerializeField] SkinColor skin = SkinColor.Light;
    [SerializeField] ShirtColor shirt = ShirtColor.Brown;
    [SerializeField] PantsColor pants = PantsColor.Dark;
    public Color swapTo;
    private Color[] hairShade = { new Color(246,202,159,255) };
    private Color[] skinShade;
    private Color[] shirtShade;
    private Color[] pantsShade;

    private void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    [Button]
    void ChangeHair()
    {
        SwapColor(SwapIndex.Outline, swapTo);
    }

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        spriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public void SwapColor(SwapIndex index, Color color)
    {
        spriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }

    public enum SwapIndex
    {
        Outline = 246,
        SkinPrim = 254,
        SkinSec = 239,
        HandPrim = 235,
        HandSec = 204,
        ShirtPrim = 62,
        ShirtSec = 70,
        ShoePrim = 253,
        ShoeSec = 248,
        Pants = 72,
    }
}
