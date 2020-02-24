using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineSupermarket.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace OnlineSupermarket.Data
{
    public class ProductRepository : IProductRepository
    {

        private readonly ProductContext _context;

        public ProductRepository(ProductContext ctx)
        {
            _context = ctx;
        }

        public async Task<ProductCategoryViewModel> GetProducts(string searchString, string productCategory, 
            int page, int numResults, ProductSorting sorting = ProductSorting.Popular)
        {
            // returns list of all unique categories in db. For Categories SelectList in ViewModel
            IQueryable<string> categoryQuery = from m in _context.Product
                                               orderby m.Category
                                               select m.Category;
            // list of available sorting options
            var sortingList = new List<ProductSorting>() {ProductSorting.Default, ProductSorting.Rating, ProductSorting.Popular, };

            // Build Products query
            int skip = page == 0 ? 0 : numResults * page;
            var products = (from m in _context.Product select m).Skip(skip).Take(numResults);

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(productCategory))
            {
                products = products.Where(x => x.Category == productCategory);
            }

            if (sorting == ProductSorting.Popular)
            {
                products = products.OrderByDescending(x => x.Popularity);
            }
            else if (sorting == ProductSorting.Rating)
            {
                products = products.OrderByDescending(x => x.Rating);
            }

            var catVM = new ProductCategoryViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Products = products.ToList(),
                SearchString = searchString,
                ProductCategory = productCategory,
                CurrentPage = page,
                NumberOfResults = numResults,
                Sorting = new SelectList(sortingList),
                SortBy = sorting.ToString()
            };

            return catVM;
        }

        /// <summary>
        /// Returns a <see cref="Product"/> matching the given Id, or null if none could be found.
        /// </summary>
        public async Task<Product> GetProductById(int id)
        {
           return await _context.Product
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<string>> GetProductCategories()
        {
            // returns list of all unique categories in db. For Categories SelectList in ViewModel
            IQueryable<string> categoryQuery = from m in _context.Product
                                               orderby m.Category
                                               select m.Category;

            return await categoryQuery.Distinct().ToListAsync();
        }
    }
}
