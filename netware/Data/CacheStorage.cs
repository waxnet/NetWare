using System;
using System.Collections.Generic;
using System.Linq;

namespace NetWare.Storage;

public static class CacheStorage
{
    public static string CreateCacheKey(params object[] values)
    {
        return string.Join("_", values.Select(x => x.ToString()));
    }
}

public sealed class CacheStorage<T>
{
    private readonly Dictionary<string, T> _cache = [];

    public T GetOrCreate(string key, Func<T> create)
    {
        if (_cache.TryGetValue(key, out T cachedInstance))
            return cachedInstance;

        T instance = create();
        _cache[key] = instance;
        return instance;
    }
}
