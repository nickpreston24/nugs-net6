using System.Runtime.Caching;
using CodeMechanic.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace nugsnet6.Controllers;

// Adapted from:
// https://stackoverflow.com/questions/53207218/how-to-access-imemorycache-from-c-sharp-and-javascript
public class CacheController : Controller
{
    private readonly MemoryCache cache;

    public CacheController()
    {
        cache = MemoryCache.Default;

        // Define cache key and data
        string cacheKey = "FullName";
        string cachedData = "Nick Preston";

        // Add data to the cache with an expiration time of 5 minutes
        CacheItemPolicy cachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };

        cache.Add(cacheKey, cachedData, cachePolicy);
        cache.Dump(nameof(cache));
    }

    [HttpGet("{key}")]
    public IActionResult GetCacheValue(string key)
    {
        Console.WriteLine("key :>> " + key);
        var cacheValue = cache.Get(key);
        return Json(cacheValue);
    }
}