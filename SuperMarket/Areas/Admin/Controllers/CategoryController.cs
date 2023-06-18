using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
using SuperMarket.Repository;
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
            //List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id != null)
            {
                if (id <= 0)
                {
                    return NotFound();
                }
                Category? categoryFromDB = _unitOfWork.Category.Get(x => x.Id == id);

                if (categoryFromDB == null)
                {
                    return NotFound();
                }
                return View(categoryFromDB);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Upsert(Category _category)
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


        #region API

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CategoryToDelete = _unitOfWork.Category.Get(u => u.Id == id);

            if (CategoryToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Category.Remove(CategoryToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category.GetAll();
            return Json(new { data = categoryList });
        }

        #endregion


    }
}
