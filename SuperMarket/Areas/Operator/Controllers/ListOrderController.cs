using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
using SuperMarket.Models.ViewModels;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;
using System.Data;

namespace SuperMarket.Areas.Operator.Controllers
{
    //[Authorize(Roles = SuperMarketRoles.Role_Operator)]
    [Area("Operator")]
    public class ListOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ListOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API

        public IActionResult GetAll()
        {
            var OrderList = _unitOfWork.Order.GetAll(includeProperties: "ProductOrders"); 


            var formattedList = OrderList.Select(Order => new
            {

                Id = Order.Id,
                Status = Order.Status,
                
                
                ProductOrders = Order.ProductOrders.Select(productOrder => new
                {
                    ProductId = productOrder.ProductId,
                    OrderId = productOrder.OrderId,
                    Quantity = productOrder.Quantity
                    
                })
            });

            return Json(new { data = formattedList });
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                if (id <= 0)
                {
                    return NotFound();
                }
                Order? OrderFromDB = _unitOfWork.Order.Get(x => x.Id == id);
                OrderVM? OrderVMFromDB = new()
                {
                    Order = OrderFromDB,
                    //ProductOrder = _unitOfWork.ProductRepository.GetAll().Select(i => 
                    //IEnumerable)
                };

                if (OrderFromDB == null)
                {
                    return NotFound();
                }
                return View(OrderFromDB);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Details(Category _category)
        {
            if (ModelState.IsValid)
            {
                if (_category.Id == 0 || _category.Id == null)
                    _unitOfWork.Category.Add(_category);
                else
                    _unitOfWork.Category.Update(_category);

                _unitOfWork.Save();
                TempData["success"] = "Category saved successfully";
            }
            else
            {
                TempData["error"] = "Error creating category";
            }
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var OrderList = _unitOfWork.Order.GetAll(includeProperties: "ProductOrders");

        //    var formattedList = ProductList.Select(Order => new
        //    {
        //        Id = Order.Id,
        //        Status = Order.s


        //    })

        //    return Json(new { data = ProductList });
        //}


        #endregion
    }
}




