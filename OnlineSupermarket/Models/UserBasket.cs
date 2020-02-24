using System.Collections.Generic;

namespace OnlineSupermarket.Models
{
    public class UserBasket
    {

        /// <summary>
        /// Key = product id, Value = quantity of that product.
        /// </summary>
        public Dictionary<int, int> BasketItems { get; set; }
    }
}
