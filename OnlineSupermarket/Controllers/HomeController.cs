using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineSupermarket.Data;
using OnlineSupermarket.Models;

namespace OnlineSupermarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepos, IPromotionRepository promoRepos)
        {
            _productRepository = productRepos;
            _promotionRepository = promoRepos;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // HACKY SOLUTION TO ENSURE SESSION ID DOESN'T CHANGE
            // see https://stackoverflow.com/questions/2874078/asp-net-session-sessionid-changes-between-requests
            HttpContext.Session.Set("id", new byte[0]);

            var viewModel = new HomeViewModel()
            {
                Categories = new SelectList(await _productRepository.GetProductCategories()),
                Promotions = await _promotionRepository.GetOngoingPromotions(6)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
