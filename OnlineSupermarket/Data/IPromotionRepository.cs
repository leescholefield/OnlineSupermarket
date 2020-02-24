using OnlineSupermarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineSupermarket.Data
{
    public interface IPromotionRepository
    {
        /// <summary>
        /// Returns a random selection of current <see cref="Promotion"/>s.
        /// </summary>
        /// <param name="limit">Maximum number of promotions to return.</param>
        /// <returns>Collection of current Promotions; returns an empty collection if there are no promotions.</returns>
        Task<IEnumerable<Promotion>> GetOngoingPromotions(int limit);

        Task<Promotion> GetPromotionById(int id);
    }
}
