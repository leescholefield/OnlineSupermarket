using Microsoft.EntityFrameworkCore;
using OnlineSupermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSupermarket.Data
{
    public class PromotionRepository : IPromotionRepository
    {

        private ProductContext _context;

        public PromotionRepository(ProductContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Returns a collection of ongoing <see cref="Promotion"/>s; if there are no current promotions this will return an empty 
        /// collection.
        /// </summary>
        /// <param name="limit">Maximum number of promotions to return.</param>
        public async Task<IEnumerable<Promotion>> GetOngoingPromotions(int limit)
        {
            // get promotions
            var promoQuery = (from m in _context.Promotions select m).Take(limit);
            var promos = await promoQuery.ToDictionaryAsync( v => v.ID);

            // get promo items
            var promoIDs = promos.Keys;
            var promoItemsQuery = (from m in _context.PromotionItems select m).Where(p => promoIDs.Contains(p.PromotionID));
            var promoItems = await promoItemsQuery.ToListAsync();

            var productIDs = promoItems.Select(p => p.ProductID);
            var productQuery = (from m in _context.Product select m).Where(p => productIDs.Contains(p.ID));
            var products = await productQuery.ToDictionaryAsync(v => v.ID);

            // sort promo items
            foreach (var p in promoItems)
            {
                promos[p.PromotionID].Items.Add(products[p.ProductID]);
            }

            return promos.Values;
        }

        public async Task<Promotion> GetPromotionById(int id)
        {
            var promo = await _context.Promotions.FirstOrDefaultAsync(p => p.ID == id);

            var promoItemsQuery = (from m in _context.PromotionItems select m).Where(p => p.PromotionID == promo.ID);
            var promoItems = await promoItemsQuery.ToListAsync();

            var productIDs = promoItems.Select(p => p.ProductID);
            var productQuery = (from m in _context.Product select m).Where(p => productIDs.Contains(p.ID));
            var products = await productQuery.ToListAsync();

            promo.Items = products;
            return promo;
        }
    }
}
