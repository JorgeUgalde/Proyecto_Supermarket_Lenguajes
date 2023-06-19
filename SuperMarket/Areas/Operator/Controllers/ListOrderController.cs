using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
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
                    OrderId = productOrder.OrderId
                    
                })
            });

            return Json(new { data = formattedList });
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




