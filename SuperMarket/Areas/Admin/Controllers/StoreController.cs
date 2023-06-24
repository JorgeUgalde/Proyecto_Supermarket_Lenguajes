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
        /*
         * Admin: admin@ucr.ac.cr Usuario#10
		 * 
		 * Customer: customer@ucr.ac.cr Usuario#10
		 */
        private readonly IUnitOfWork _unitOfWork;
        private readonly int SuperMarketId = 1;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            Store store = _unitOfWork.Store.Get(x => x.Id == SuperMarketId);
            return View(store);
        }

        public ActionResult CloseSuperMarket()
        {
            UpdateSuperMarketStatus(0); // Set the supermarket status to closed
            return RedirectToAction("Index");
        }

        public ActionResult OpenSuperMarket()
        {
            UpdateSuperMarketStatus(1); // Set the supermarket status to open
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
            Store store = _unitOfWork.Store.Get(x => x.Id == SuperMarketId);
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

            Store store = _unitOfWork.Store.Get(x => x.Id == 1);
            isOpenState = store?.IsOpen == 1;

            if (isOpenState == false)
            {
                // SuperMarket is closed, redirect to the "SuperMarketClosed" view in Customer/Views/Shared
                context.Result = new ViewResult
                {
                    ViewName = "SuperMarketClosed"
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
