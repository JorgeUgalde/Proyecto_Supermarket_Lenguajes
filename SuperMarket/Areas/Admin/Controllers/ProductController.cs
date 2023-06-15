using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Models;
using SuperMarket.Models.ViewModels;
using SuperMarket.Repository.Interfaces;

namespace SuperMarket.Areas.Admin.Controllers
{
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
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }



        [HttpPost]
        public IActionResult Upsert(ProductVM _productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\vehicles");
                    var extension = Path.GetExtension(file.FileName);



                    if (_productVM.Product.PictureUrl != null) //Para las modificaciones
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _productVM.Product.PictureUrl);
                        if (System.IO.File.Exists(oldImageUrl))
                        {
                            System.IO.File.Delete(oldImageUrl);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    _productVM.Product.PictureUrl = @"images\products\" + fileName + extension;

                }

                if (_productVM.Product.Id == 0)
                    _unitOfWork.Product.Add(_productVM.Product);
                else
                    _unitOfWork.Product.Update(_productVM.Product);

                _unitOfWork.Save();
                TempData["Success"] = "Product saved successfully";
            }

            return RedirectToAction("Index");
        }



        #region API


        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.Product.GetAll(includeProperties: "Product,Product.Category");
            return Json(new { data = ProductList });
        }



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;


            var productToDelete = _unitOfWork.Product.Get(u => u.Id == id);

            if (productToDelete == null)
                return Json(new { success = false, message = "Error while deleting" });


            var oldImageUrl = Path.Combine(wwwRootPath, productToDelete.PictureUrl);

            if (System.IO.File.Exists(oldImageUrl))
            {
                System.IO.File.Delete(oldImageUrl);
            }

            _unitOfWork.Product.Remove(productToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion

    }
}
