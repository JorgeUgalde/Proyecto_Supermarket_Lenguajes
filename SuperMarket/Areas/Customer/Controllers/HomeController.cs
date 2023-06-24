using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SuperMarket.Areas.Admin.Controllers;

namespace SuperMarket.Areas.Customer.Controllers
{
    [Area("Customer")]
    [TypeFilter(typeof(SuperMarketStateAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _unitOfWork.ProductRepository.GetAll();
            return View(ProductList);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            Product Product = _unitOfWork.ProductRepository.Get(v => v.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
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