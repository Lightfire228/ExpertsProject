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

		//public ActionResult Search() {
			
		//	return View();
		//}
		
		[HttpPost]
		public ActionResult Search(SearchStringViewModel model) {

			string search = model.SearchText;

			IEnumerable<Expert> experts = _dbContext.Experts.ToList();
			IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();
			IEnumerable<SearchViewModel> models;

			experts = from expert in experts
					  from str in expert.Keywords.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries)
					  where expert.Validated == true && search == str
					  select expert;

			models = from expert in experts
					 join user in users on expert.Id equals user.Id
					 select new SearchViewModel { Name = user.Name, Expertise = expert.ExpertiseCatagory, Id = expert.Id };

			return View(model);
		}
    }
}