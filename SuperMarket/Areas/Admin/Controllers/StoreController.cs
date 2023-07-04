using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;

namespace SuperMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = SuperMarketRoles.Role_Admin)]
    [Area("Admin")]
    public class StoreController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            Store store = _unitOfWork.Store.Get(x => x.Id == SuperMarketState.SuperMarketId);
            return View(store);
        }

        public ActionResult CloseSuperMarket()
        {
            UpdateSuperMarketStatus(SuperMarketState.Close_SuperMarket); // Set the supermarket status to closed
            TempData["success"] = "Supermarket closed successfully";
            return RedirectToAction("Index");
        }

        public ActionResult OpenSuperMarket()
        {
            UpdateSuperMarketStatus(SuperMarketState.Open_SuperMarket); // Set the supermarket status to open
            TempData["success"] = "Supermarket opened successfully";
            return RedirectToAction("Index");
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var stores = _unitOfWork.Store.GetAll();
            return Json(new { data = stores });
        }

        #endregion

        private void UpdateSuperMarketStatus(int status)
        {
            Store store = _unitOfWork.Store.Get(x => x.Id == SuperMarketState.SuperMarketId);
            if (store != null)
            {
                store.IsOpen = status;
                _unitOfWork.Store.Update(store);
                _unitOfWork.Save();
            }
        }
    }


    public class SuperMarketStateAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuperMarketStateAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isOpenState = false;

            Store store = _unitOfWork.Store.Get(x => x.Id == SuperMarketState.SuperMarketId);
            isOpenState = store?.IsOpen == 1;

            if (isOpenState == false)
            {
                context.Result = new JsonResult(403);
            }

            base.OnActionExecuting(context);
        }
    }
}
