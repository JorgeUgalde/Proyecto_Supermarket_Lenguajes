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

        //[HttpGet]
        //public IActionResult AddToCart(int id)
        //{

        //    string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
        //    string[] Separado = cadena.Split('/');
        //    string Final = Separado[Separado.Length - 1];


        //    //Product Product = _unitOfWork.ProductRepository.Get(v => v.Id == id);

        //    //if (Product == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View("Index");
        //}

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
    }
}