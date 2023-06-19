using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMarket.Models;
using SuperMarket.Models.ViewModels;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;

namespace SuperMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = SuperMarketRoles.Role_Admin)]
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private IWebHostEnvironment _webHostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            ProductVM ProductVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(ProductVM);
            }
            ProductVM.Product = _unitOfWork.ProductRepository.Get(x => x.Id == id);
            return View(ProductVM);
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM _ProductVM)
        {
            if (ModelState.IsValid)
            {
                if (_ProductVM.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(_ProductVM.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(_ProductVM.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product saved succesfully";
            }
            else
            {
                TempData["error"] = "Error saving product";
            }
            return RedirectToAction("Index");
        }



        #region API


        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category");
            return Json(new { data = ProductList });
        }



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;


            var productToDelete = _unitOfWork.ProductRepository.Get(u => u.Id == id);

            if (productToDelete == null)
                return Json(new { success = false, message = "Error while deleting" });


            var oldImageUrl = Path.Combine(wwwRootPath, productToDelete.PictureUrl);

            if (System.IO.File.Exists(oldImageUrl))
            {
                System.IO.File.Delete(oldImageUrl);
            }

            _unitOfWork.ProductRepository.Remove(productToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion

    }
}
