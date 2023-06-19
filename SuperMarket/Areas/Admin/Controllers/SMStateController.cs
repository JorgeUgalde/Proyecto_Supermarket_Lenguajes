using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Repository.Interfaces;
using SuperMarket.Utilities;
using System.Data;

namespace SuperMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = SuperMarketRoles.Role_Admin)]
    [Area("Admin")]
    public class SMStateController : Controller
	{
        /*
         * Admin: admin@ucr.ac.cr Usuario#10
		 * 
		 * Customer: customer@ucr.ac.cr Usuario#10
		 */
        private readonly IUnitOfWork _unitOfWork;

		public SMStateController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			return View();
		}

	}
}
