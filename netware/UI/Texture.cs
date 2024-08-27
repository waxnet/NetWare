using NetWare.Storage;
using UnityEngine;

namespace NetWare.UI;

public static class Texture
{
    // cache system
    private static CacheStorage<Texture2D> _textureCache = new();

    // methods
    public static Texture2D New(float r, float g, float b)
    {
        var cacheKey = CacheStorage.CreateCacheKey(r, g, b);
        var texture = _textureCache.GetOrCreate(cacheKey, () =>
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(r, g, b));
            texture.Apply();

            return texture;
        });

        return texture;
    }

    public static Texture2D NewTransparent()
    {
        // check cache
        var cacheKey = "transparent";
        var texture = _textureCache.GetOrCreate(cacheKey, () =>
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(1, 1, new Color(0, 0, 0, 0));
            texture.Apply();

            return texture;
        });

        return texture;
    }

    public static Texture2D NewBorder(float borderR, float borderG, float borderB, float centerR, float centerG, float centerB)
    {
        // check cache
        var cacheKey = CacheStorage.CreateCacheKey(borderR, borderG, borderB, centerR, centerG, centerB);
        var texture = _textureCache.GetOrCreate(cacheKey, () =>
        {
            var borderColor = new Color(borderR, borderG, borderB);

            var texture = new Texture2D(3, 3, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, borderColor);
            texture.SetPixel(1, 0, borderColor);
            texture.SetPixel(2, 0, borderColor);
            texture.SetPixel(0, 1, borderColor);
            texture.SetPixel(1, 1, new Color(centerR, centerG, centerB));
            texture.SetPixel(2, 1, borderColor);
            texture.SetPixel(0, 2, borderColor);
            texture.SetPixel(1, 2, borderColor);
            texture.SetPixel(2, 2, borderColor);
            texture.Apply();

            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;

            return texture;
        });

        return texture;
    }
}
