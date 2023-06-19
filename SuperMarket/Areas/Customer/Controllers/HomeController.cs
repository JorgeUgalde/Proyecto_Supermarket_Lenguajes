using Microsoft.AspNetCore.Mvc;
using SuperMarket.Areas.Admin.Controllers;
using SuperMarket.Models;
using System.Diagnostics;
using static SuperMarket.Areas.Admin.Controllers.StoreController;

namespace SuperMarket.Areas.Customer.Controllers
{
    [Area("Customer")]
    [SuperMarketState]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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