using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Client;
using SuperMarket.Models;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;
using System.Data;

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

        public static bool superMarketState { get; private set; }

        public static Store store { get; private set; }

		public StoreController(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
            Store store = _unitOfWork.Store.Get(x => x.Id == 1);
            
            if (store.IsOpen == 1)
            {
                superMarketState = true;
            }
            else
            {
                superMarketState = false;
            }
		}

        //public static bool GetSuperMarketState()
        //{
        //    //int superMarketState = GetSuperMarketStateInt(Get_unitOfWork());
        //    if (superMarketState == 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private static int GetSuperMarketStateInt(IUnitOfWork _unitOfWork)
        {
            store = _unitOfWork.Store.Get(x => x.Id == 1);
            return store.IsOpen;
        }

		public ActionResult Index()
		{
            Store store = _unitOfWork.Store.Get(x => x.Id == 1);
            return View(store);
		}

        public ActionResult CloseSuperMarket()
        {
            Store store = _unitOfWork.Store.Get(x => x.Id == 1);
            store.IsOpen = 0;
            _unitOfWork.Store.Update(store);
            _unitOfWork.Save();
            superMarketState = false;
            return RedirectToAction("Index");
        }

        public ActionResult OpenSuperMarket()
        {
            Store store = _unitOfWork.Store.Get(x => x.Id == 1);
            store.IsOpen = 1;
            _unitOfWork.Store.Update(store);
            _unitOfWork.Save();
            superMarketState = true;
            return RedirectToAction("Index");
        }

        

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var Store = _unitOfWork.Store.GetAll();
            return Json(new { data = Store });
        }

        #endregion

        public class SuperMarketStateAttribute : ActionFilterAttribute
        {



            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                bool superMarketOpen = GetSuperMarketState(); // Obtiene el estado actual del supermercado

                if (!superMarketOpen)
                {
                    // El supermercado está cerrado, redirige a una página de error en Customer/Views/Shared
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "SuperMarketClosed"
                    };
                }

                base.OnActionExecuting(filterContext);
            }

            public static bool GetSuperMarketState()
            {
                if (StoreController.superMarketState == true)
                {
                    return true;
                }
                return false;
            }
        }
    }

}
