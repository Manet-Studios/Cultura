using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VillagerColorizer : MonoBehaviour
{

    Texture2D mColorSwapTex;
    Color[] spriteColors;
    SpriteRenderer renderer;
    public Texture2D[] HairPalettes = new Texture2D[5];
    public Texture2D[] SkinPalettes = new Texture2D[3];
    public Texture2D[] ShirtPalettes = new Texture2D[2];
    public Texture2D[] PantsPalettes = new Texture2D[2];

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
        renderer = GetComponent<SpriteRenderer>();
    }

    [Button]
    void UpdateSprite()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.material.SetTexture("_HairPaletteTex", HairPalettes[(int)hair]);        
    }
}
