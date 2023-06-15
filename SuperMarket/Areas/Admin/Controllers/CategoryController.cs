using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using System.Data;

namespace SuperMarket.Areas.Admin.Controllers
{
   // [Authorize(Roles = CarDealerRoles.Role_Admin)]
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;
        }



        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}
