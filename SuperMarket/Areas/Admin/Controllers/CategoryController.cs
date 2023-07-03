using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
using SuperMarket.Repository;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;
using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SuperMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = SuperMarketRoles.Role_Admin)]
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                TempData["error"] = "Error saving category";
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
                return Json(new { success = false, message = "Error while deleting category" });
            }

            _unitOfWork.Category.Remove(CategoryToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Category deleted successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitOfWork.Category.GetAll(includeProperties: "Products");

            var formattedList = categoryList.Select(category => new
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select(product => new
                {
                    Id = product.Id,
                    BarCode = product.BarCode,
                    Name = product.Name,
                    Price = product.Price,
                    InStock = product.InStock,
                    IsActive = product.IsActive,
                    PictureUrl = product.PictureUrl,
                    Unit = product.Unit
                })
            });

            return Json(new { data = formattedList });
        }


        #endregion


    }
}



