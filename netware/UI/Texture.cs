using System.Collections.Generic;
using UnityEngine;

namespace NetWare.UI;

public static class Texture
{
    // cache system
    private static Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();

    private static string GetCacheKey(params float[] values)
    {
        return string.Join("_", values);
    }

    // methods
    public static Texture2D New(float r, float g, float b)
    {
        // check cache
        string cacheKey = GetCacheKey(r, g, b);
        if (cache.TryGetValue(cacheKey, out Texture2D cachedTexture))
            return cachedTexture;

        // make texture
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, new Color(r, g, b));
        texture.Apply();

        // return texture and add to cache
        cache[cacheKey] = texture;
        return texture;
    }

    public static Texture2D NewTransparent()
    {
        // check cache
        string cacheKey = "transparent";
        if (cache.TryGetValue(cacheKey, out Texture2D cachedTexture))
            return cachedTexture;

        // make texture
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, new Color(0, 0, 0, 0));
        texture.Apply();

        // return texture and add to cache
        cache[cacheKey] = texture;
        return texture;
    }

    public static Texture2D NewBorder(float borderR, float borderG, float borderB, float centerR, float centerG, float centerB)
    {
        // check cache
        string cacheKey = GetCacheKey(borderR, borderG, borderB, centerR, centerG, centerB);
        if (cache.TryGetValue(cacheKey, out Texture2D cachedTexture))
            return cachedTexture;

        // make texture
        Color borderColor = new Color(borderR, borderG, borderB);

        Texture2D texture = new Texture2D(3, 3, TextureFormat.RGBA32, false);
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

        // return texture and add to cache
        cache[cacheKey] = texture;
        return texture;
    }
}
