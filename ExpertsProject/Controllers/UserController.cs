using ExpertsProject.Models;
using ExpertsProject.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertsProject.Controllers
{
    public class UserController : Controller
    {

		private ApplicationDbContext _dbContext = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Search() {
			
			return RedirectToAction("Index");
		}
		
		[HttpPost]
		public ActionResult Search(SearchStringViewModel model) {

			// If you want to redirect to login if not logged in
			//if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
				//return RedirectToAction("Login", "Account");

			string search = model.SearchText;

			IEnumerable<Expert> experts = _dbContext.Experts.ToList();
			IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();
			IEnumerable<SearchViewModel> models;


			experts = from expert in experts
					  from str in expert.Keywords.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
					  where expert.Validated && search.ToLower().Equals(str.ToLower())
					  select expert;

			models = from expert in experts
					 join user in users on expert.Id equals user.Id
					 select new SearchViewModel { Name = user.Name, Expertise = expert.ExpertiseCatagory, Id = expert.Id };

			return View(models);
		}
    }
}