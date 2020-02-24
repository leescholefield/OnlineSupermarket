using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineSupermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineSupermarket.Controllers
{
    /// <summary>
    /// Contains useful methods that many Controllers will need
    /// </summary>
    public abstract class BaseController : Controller
    {

        protected readonly IMemoryCache _cache;

        public BaseController(IMemoryCache cache)
        {
            _cache = cache;
        }

        protected int SaveIdToCache(int id)
        {
            var sessionId = HttpContext.Session.Id;

            if (!_cache.TryGetValue(sessionId, out UserBasket userBasket))
            {
                userBasket = new UserBasket
                {
                    BasketItems = new Dictionary<int, int>() { { id, 1 } }
                };
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(sessionId, userBasket, cacheEntryOptions);
            }
            else
            {
                if (userBasket.BasketItems.ContainsKey(id))
                    throw new InvalidOperationException("Attempting to add an ID to the cache that already exists. If you want to increment " +
                        "that Id you should instead call SetQuantity");

                userBasket.BasketItems.Add(id, 1);
            }

            return userBasket.BasketItems.Count;
        }

        protected int GetNumberItemsInBasket()
        {
            var sessionId = HttpContext.Session.Id;

            var basket = _cache.Get<UserBasket>(sessionId);
            return basket == null ? 0 : basket.BasketItems.Count;
        }

        protected int[] GetProductIdsInBasket()
        {
            var sessionId = HttpContext.Session.Id;

            var basket = _cache.Get<UserBasket>(sessionId);
            return basket == null ? new int[0] : basket.BasketItems.Keys.ToArray();
        }
    }
}
