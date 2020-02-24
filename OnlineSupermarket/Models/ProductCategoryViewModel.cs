using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OnlineSupermarket.Models
{
    public class ProductCategoryViewModel : BaseViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// User's selected category.
        /// </summary>
        public string ProductCategory { get; set; }

        public SelectList Sorting { get; set; }

        /// <summary>
        /// User's selected sorting.
        /// </summary>
        public string SortBy { get; set; }

        public string SearchString { get; set; }

        public int CurrentPage { get; set; }

        public int NumberOfResults { get; set; }
    }
}
