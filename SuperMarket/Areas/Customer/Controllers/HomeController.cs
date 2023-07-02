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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Categories");
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            foreach (var item in productList)
            {
                if (item.PictureUrl != null)
                {
                    item.PictureUrl = $"{baseUrl}/{item.PictureUrl}";
                }
            }

            var formattedList = productList.Select(Product => new
            {
                Id = Product.Id,
                BarCode = Product.BarCode,
                Name = Product.Name,
                Price = Product.Price,
                InStock = Product.InStock,
                IsActive = Product.IsActive,
                PictureUrl = Product.PictureUrl,
                Unit = Product.Unit,
                Categories = Product.Categories.Select(Category => new
                {
                    Id = Category.Id,
                    Name = Category.Name
                })
            });

            return Json(new { data = formattedList });
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var productBD = _unitOfWork.ProductRepository.Get(u => u.Id == id, includeProperties: "Categories");
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            if (productBD.PictureUrl != null)
            {
                productBD.PictureUrl = $"{baseUrl}/{productBD.PictureUrl}";
            }

            var formattedProduct = new
            {
                Id = productBD.Id,
                BarCode = productBD.BarCode,
                Name = productBD.Name,
                Price = productBD.Price,
                InStock = productBD.InStock,
                IsActive = productBD.IsActive,
                PictureUrl = productBD.PictureUrl,
                Unit = productBD.Unit,
                Categories = productBD.Categories.Select(Category => new
                {
                    Id = Category.Id,
                    Name = Category.Name
                })
            };

            return Json(new { data = formattedProduct });
        }
        #endregion
    }
}