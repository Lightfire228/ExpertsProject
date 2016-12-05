using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpertsProject.Models;
using ExpertsProject.Models.UserViewModels;
using Microsoft.AspNet.Identity;

namespace ExpertsProject.Controllers {
	public class AdminController : Controller {
		private ApplicationDbContext _dbContext;

		public AdminController() {
			_dbContext = new ApplicationDbContext();
		}
		// GET: Admin
		public ActionResult Index() {
			
			if (!isAdmin())
				return View("Oops");

			return View();
		}
		public ActionResult AdminVerify() {

			if (!isAdmin())
				return View("Oops");

			IEnumerable<Expert> experts = _dbContext.Experts.ToList();
			IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();
			IEnumerable<SearchViewModel> models;


			experts = from expert in experts
					  where !expert.Validated
					  select expert;

			models = from expert in experts
					 join user in users on expert.Id equals user.Id
					 select new SearchViewModel { Name = user.Name, Expertise = expert.ExpertiseCatagory, Id = expert.Id };

			return View(models);
		}
		public ActionResult AdminDeactivate() {

			if (!isAdmin())
				return View("Oops");

			IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();

			users = from user in users
					where user.ActiveStatus
					select user;

			return View(users);
		}
		public ActionResult AdminActivate() {

			if (!isAdmin())
				return View("Oops");

			IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();

			users = from user in users
					where !user.ActiveStatus
					select user;

			return View(users);
		}

		public ActionResult Verify(ApplicationUser expert) {

			if (!isAdmin())
				return View("Oops");

			var expertInDb = _dbContext.Experts.Find(expert.Id);

			expertInDb.Validated = true;

			_dbContext.SaveChanges();

			return RedirectToAction("AdminVerify");
		}

		public ActionResult Deactivate(ApplicationUser expert) {

			if (!isAdmin())
				return View("Oops");

			var expertInDb = _dbContext.Users.Find(expert.Id);
			expertInDb.ActiveStatus = false;
			_dbContext.SaveChanges();
			return RedirectToAction("AdminDeactivate");
		}
		public ActionResult Activate(ApplicationUser expert) {

			if (!isAdmin())
				return View("Oops");

			var expertInDb = _dbContext.Users.Find(expert.Id);
			expertInDb.ActiveStatus = true;
			_dbContext.SaveChanges();
			return RedirectToAction("AdminActivate");
		}


		public ApplicationUser getUser() {
			return _dbContext.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
		}

		public bool isAdmin() {
			return System.Web.HttpContext.Current.User.Identity.IsAuthenticated && getUser().IsAdmin;
		}

	}
}