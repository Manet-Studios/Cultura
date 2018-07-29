using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomAssetPreProcessor : AssetPostprocessor
{
    private const int PPU = 64;

    private void OnPreprocessTexture()
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        if (lowerCaseAssetPath.IndexOf("/_preprocessing/") == -1)
            return;

        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.mipmapEnabled = false;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.spritePixelsPerUnit = PPU;
        textureImporter.textureType = TextureImporterType.Sprite;
    }

    private void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();

        if (lowerCaseAssetPath.IndexOf("/_preprocessing/") == -1)
            return;

        TextureImporter textureImporter = (TextureImporter)assetImporter;

        int endOfPath = lowerCaseAssetPath.LastIndexOf('/');

        string fileName = assetPath.Substring(endOfPath + 1);

        int endOfName = fileName.LastIndexOf('.');
        string fullName = fileName;
        fileName = fileName.Substring(0, endOfName);

        string[] nameFragments = fileName.Split('_', '-');

        if (nameFragments.Length == 1) // Single Sprite
        {
            textureImporter.spriteImportMode = SpriteImportMode.Single;
        }
        else if (nameFragments.Length == 2) //Animation
        {
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;

            int frames = Mathf.Max(texture.height, texture.width) / Mathf.Min(texture.height, texture.width);

            int spriteWidth = texture.width / frames;
            int spriteHeight = texture.height;

            List<SpriteMetaData> metas = new List<SpriteMetaData>();

            for (int r = 0; r < frames; ++r)
            {
                SpriteMetaData meta = new SpriteMetaData
                {
                    rect = new Rect(r * spriteWidth, 0, spriteWidth, spriteHeight),
                    name = nameFragments[0] + " " + nameFragments[1] + " " + r
                };
                metas.Add(meta);
            }

            textureImporter.spritesheet = metas.ToArray();
        }
        else if (nameFragments.Length == 3) //Animation
        {
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            int frames = int.Parse(nameFragments[2]);

            int spriteWidth = texture.width / frames;
            int spriteHeight = texture.height;

            List<SpriteMetaData> metas = new List<SpriteMetaData>();

            for (int r = 0; r < frames; ++r)
            {
                SpriteMetaData meta = new SpriteMetaData
                {
                    rect = new Rect(r * spriteWidth, 0, spriteWidth, spriteHeight),
                    name = nameFragments[0] + " " + nameFragments[1] + " " + nameFragments[2]
                };
                metas.Add(meta);
            }

            textureImporter.spritesheet = metas.ToArray();
        }

        EditorApplication.delayCall += () =>
        {
            string newPath = "Assets/_Processed/" + fullName;
            string error = AssetDatabase.MoveAsset(assetPath, newPath);
        };
    }

    public void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    {
    }
}