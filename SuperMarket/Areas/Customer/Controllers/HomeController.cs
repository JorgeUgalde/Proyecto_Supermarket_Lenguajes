using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SuperMarket.Areas.Admin.Controllers;
using SuperMarket.Models.ViewModels;
using SuperMarket.Utilities;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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


        // get json from externar origin and return it
        [HttpPost]

        public async Task<IActionResult> GetJsonFromExternalOrigin()
        {
            var client = new HttpClient();
            var result = await client.GetAsync("https://localhost:44380/api/Products");
            var json = await result.Content.ReadAsStringAsync();
            return Json(new { data = json });
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


        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDataVM orderData)
        {
            if (orderData == null)
            {
                return Json(new { success = false, message = "Error while Creating" });
            }

            ApplicationUser user = _unitOfWork.ApplicationUser.Get(u => u.UserIdentification == orderData.UserId);

            // 1. Crear la orden
            Order order = new Order
            {
                UserIdentification = orderData.UserId,
                Status = OrderStatus.New,
                ApplicationUser = user
            };

            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();

            // 2. Obtener el ID de la orden recién creada
            int orderId = order.Id;

            //// 3. Insertar los productos relacionados en la tabla intermedia
            foreach (Dictionary<string, int> productOrder in orderData.ProductsData.ToList())
            {
                Product productFromDB = _unitOfWork.ProductRepository.Get(u => u.Id == productOrder["ProductId"]);

                productFromDB.InStock -= productOrder["Quantity"];
                _unitOfWork.ProductRepository.Update(productFromDB);
                _unitOfWork.Save();

                ProductOrder newProductOrder = new ProductOrder
                {
                    OrderId = orderId,
                    ProductId = productOrder["ProductId"],
                    Quantity = productOrder["Quantity"],

                };

                _unitOfWork.ListOrder.Add(newProductOrder);
            }

            _unitOfWork.Save();

            return Json(new { success = true, message = "Order created successfully." });
        }




        [HttpPost]
        public IActionResult CreateUser([FromBody] ApplicationUser user)
        {
            if (user == null)
            {
                return Json(new { success = false, message = "Error while Creating" });
            }

            int userId = user.UserIdentification;

            Boolean userExists = false;

            if (userId == 0)
            {
                userId = LastUserId.Id + 1;
                LastUserId.Id = userId;

            }
            else
            {
                userExists = true;
            }

            // 1. Crear la orden
            ApplicationUser newUser = new ApplicationUser
            {
                UserIdentification = userId,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                StreetAddress = user.StreetAddress
            };
            if (userExists)
            {
                ApplicationUser? userDB = _unitOfWork.ApplicationUser.Get(u => u.UserIdentification == userId);
                userDB.Name = newUser.Name;
                userDB.PhoneNumber = newUser.PhoneNumber;
                userDB.Email = newUser.Email;
                userDB.StreetAddress = newUser.StreetAddress;

                _unitOfWork.ApplicationUser.Update(userDB);
                _unitOfWork.Save();

            }
            else
            {
                _unitOfWork.ApplicationUser.Add(newUser);
                _unitOfWork.Save();

            }
            return Json(new { success = true, message = "Order created successfully.", id = userId });
        }



        #endregion
    }
}