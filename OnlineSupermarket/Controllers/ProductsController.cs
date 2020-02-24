using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineSupermarket.Data;
using System;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using OnlineSupermarket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineSupermarket.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;

        public ProductsController(IProductRepository prodRepos, IPromotionRepository promoRepos, IMemoryCache cache) : base(cache)
        {
            _productRepository = prodRepos;
            _promotionRepository = promoRepos;
        }

        // GET: Products
        public async Task<IActionResult> Index(string productCategory, string searchString, int? page, int? numResults, string sortBy)
        {
            Enum.TryParse(sortBy, true, out ProductSorting sortingMethod);

            var catVM = await _productRepository.GetProducts(searchString, productCategory,
                                                        page.GetValueOrDefault(0), numResults.GetValueOrDefault(20), 
                                                        sortingMethod);

            // mark products that are already in the user basket
            var basketIds = GetProductIdsInBasket();
            foreach (var product in catVM.Products)
            {
                if (basketIds.Contains(product.ID))
                {
                    product.InBasket = true;
                }
            }

            catVM.NumberCheckoutItems = basketIds.Length;


            return View(catVM);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductById(id.Value);

            return View(product);
        }

        public async Task<IActionResult> Promotion(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var promo = await _promotionRepository.GetPromotionById(id.Value);

            var vm = new PromotionViewModel()
            {
                Promotion = promo,
                Categories = new SelectList(await _productRepository.GetProductCategories()),
            };

            return View(vm);
        }

        /// <summary>
        /// Adds the given id to the users basket. For now this is implemented using the Session object, however in the future it may be wiser 
        /// to use Cache
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Number of total items in the user's basket.</returns>
        [HttpPost]
        public IActionResult AddToBasket(int? id)
        {
            if (id == null)
            {
                return null;
            }

            int numItems = SaveIdToCache(id.Value);
            return Content(numItems.ToString());
        }



    }
}
