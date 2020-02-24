using OnlineSupermarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineSupermarket.Data
{
    public interface IProductRepository
    {

        /// <summary>
        /// Returns a <see cref="ProductCategoryViewModel"/> according to the search criteria. 
        /// </summary>
        /// <remarks>
        /// In order to effectively return paginated results the numResults param must not change since the last call. If it does this will 
        /// repeat some results.
        /// </remarks>
        /// <param name="searchTerm">Searches the database for any Product name that contains the searchTerm. If null will search for all 
        ///  Products.</param>
        /// <param name="category">Category to search for. If null will search in all categories.</param>
        /// <param name="page">Page of results to return.</param>
        /// <param name="numResults">Number of results to return</param>
        Task<ProductCategoryViewModel> GetProducts(string searchTerm, string category, int page, 
            int numResults, ProductSorting sorting = ProductSorting.Popular);

        Task<Product> GetProductById(int id);

        /// <summary>
        /// Returns a list of all product categories in the database.
        /// </summary>
        Task<IEnumerable<string>> GetProductCategories();
    }

    public enum ProductSorting
    {
         Default, Popular, Rating,
    }
}
