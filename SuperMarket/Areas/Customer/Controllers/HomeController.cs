using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SuperMarket.Areas.Admin.Controllers;
using SuperMarket.Models.ViewModels;
using SuperMarket.Utilities;

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


            // 1. Crear la orden
            Order order = new Order
            {
                ApplicationUserId = orderData.UserId,
                Status = OrderStatus.New
            };

            _unitOfWork.Order.Add(order);
            _unitOfWork.Save();

            // 2. Obtener el ID de la orden recién creada
            int orderId = order.Id;

            //// 3. Insertar los productos relacionados en la tabla intermedia
            foreach (Dictionary<string, int> productOrder in orderData.ProductsData.ToList())
            {


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

                 


         



        //[HttpPost]
        //public IActionResult CreateOrder([FromBody] JObject orderDataJson)
        //{
        //    // Convertir el JSON a un objeto C#
        //    OrderData orderData = orderDataJson.ToObject<OrderData>();

        //    // Aquí puedes procesar los datos recibidos en el JSON y realizar cualquier operación necesaria
        //    // Accede a los campos del objeto orderData y realiza las operaciones de inserción en la base de datos

        //    // Ejemplo de código para insertar una nueva orden y los productos relacionados en la tabla intermedia

        //    // 1. Crear la orden
        //    Order order = new Order
        //    {
        //        UserId = orderData.UserId
        //    };

        //    _unitOfWork.OrderRepository.Add(order);
        //    _unitOfWork.Save();

        //    // 2. Obtener el ID de la orden recién creada
        //    int orderId = order.Id;

        //    // 3. Insertar los productos relacionados en la tabla intermedia
        //    foreach (int productId in orderData.ProductIds)
        //    {
        //        OrderProduct orderProduct = new OrderProduct
        //        {
        //            OrderId = orderId,
        //            ProductId = productId
        //        };

        //        _unitOfWork.OrderProductRepository.Add(orderProduct);
        //    }

        //    _unitOfWork.Save();

        //    // Aquí puedes realizar cualquier otra acción necesaria, como redirigir a una página de éxito o devolver un mensaje JSON de confirmación

        //    return Json(new { success = true, message = "Order created successfully." });
        //}





        #endregion
    }
}