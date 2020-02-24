using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using OnlineSupermarket.Data;
using OnlineSupermarket.Models;

namespace OnlineSupermarket.Controllers
{
    /// <summary>
    /// Controller for a Checkout screen.
    /// </summary>
    public class CheckoutController : BaseController
    {

        private readonly IProductRepository _repository;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(IProductRepository repository, ILogger<CheckoutController> logger, IMemoryCache cache) : base(cache)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var basket = GetUserBasket();
            CheckoutViewModel viewModel;

            if (basket == null)
            {
                viewModel = new CheckoutViewModel
                {
                    ProductDictionary = new Dictionary<Product, int>(),
                    Total = 0,
                    Categories = new SelectList(await _repository.GetProductCategories())
                };

                return View(viewModel);
            }

            var products = await CreateProductDictionary(basket);
            var total = CalculateTotal(products);
            var vm = new CheckoutViewModel
            {
                ProductDictionary = products,
                Total = total,
                NumberCheckoutItems = basket.BasketItems.Count,
                Categories = new SelectList(await _repository.GetProductCategories())
            };

            return View(vm);
        }

        public void Checkout()
        {
            // empty the user's basket for now
            _cache.Remove(HttpContext.Session.Id);

            Response.Redirect(Url.Action("Index", "Home"));
        }

        /// <summary>
        /// Saves the given id and quantity to the <see cref="UserBasket"/> and then returns the new basket total.
        /// </summary>
        /// <returns>The basket total as a string.</returns>
        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(int id, int quantity)
        {
            var basket = GetUserBasket();

            if (quantity == 0)
            {
                basket.BasketItems.Remove(id);
            }

            if (basket.BasketItems.ContainsKey(id))
                basket.BasketItems[id] = quantity;
            else
                basket.BasketItems.Add(id, quantity);

            var products = await CreateProductDictionary(basket);
            double total = CalculateTotal(products);

            return Content(string.Format("{0:C}", total));
        }

        [HttpPost]
        public void RemoveItem(int id)
        {
            var basket = GetUserBasket();
            basket.BasketItems.Remove(id);
        }

        private UserBasket GetUserBasket()
        {
            return (UserBasket)_cache.Get(HttpContext.Session.Id);
        }

        private async Task<Dictionary<Product, int>> CreateProductDictionary(UserBasket basket)
        {
            Dictionary<Product, int> products = new Dictionary<Product, int>();

            foreach (KeyValuePair<int, int> pair in basket.BasketItems)
            {
                var product = await _repository.GetProductById(pair.Key);
                if (product != null)
                    products.Add(product, pair.Value);
            }

            return products;
        }

        private double CalculateTotal(Dictionary<Product, int> products) {
            double total = 0;

            foreach (KeyValuePair<Product, int> items in products)
            {
                total += (double)items.Key.Price * items.Value;
            }

            return total;
        }
    }
}
