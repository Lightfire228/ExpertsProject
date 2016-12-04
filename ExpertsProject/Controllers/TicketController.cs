using ExpertsProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertsProject.Controllers
{
    public class TicketController : Controller
    {
		private ApplicationDbContext _dbContext = new ApplicationDbContext();

		// GET: Ticket
		public ActionResult Index() {

			if (!isLoggedIn()) {
				return RedirectToAction("Login", "Account");
			}
			else if (isExpert()) {
				return RedirectToAction("ExpertIndex");
			}
			
            return RedirectToAction("UserIndex");
        }

		public ActionResult UserIndex() {
			
			return View();
		}

		public ActionResult ExpertIndex() {

			return View();
		}
		
		public bool isExpert(ApplicationUser user) {
			return _dbContext.Experts.Find(user.Id) != null;
		}

		public bool isExpert() {
			String Id = System.Web.HttpContext.Current.User.Identity.GetUserId();
			return isExpert(_dbContext.Users.Find(Id));
		}

		public bool isLoggedIn() {
			return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
		}
    }
}