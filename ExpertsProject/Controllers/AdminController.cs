using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpertsProject.Models;
using ExpertsProject.Models.UserViewModels;

namespace ExpertsProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _dbContext;

        public AdminController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminValidate()
        {

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
        public ActionResult AdminDeactivate()
        {

            IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();

            users = from user in users
                    where user.ActiveStatus
                    select user;

            return View(users);
        }
        public ActionResult DeactivateForm(int id)
        {
            var ticket = _dbContext.Users.SingleOrDefault(v => v.Id == id);

            if (ticket == null)
                return HttpNotFound();

            return View(ticket);
        }
        public ActionResult Verify(String id)
        {
            var expert = _dbContext.Experts.SingleOrDefault(v => v.Id == id);

            if (expert == null)
                return HttpNotFound();

            return View(expert);
        }
        public ActionResult Deactivate(ApplicationUser expert)
        {
            var expertInDb = _dbContext.Users.SingleOrDefault(v => v.Id == expert.Id);

            if (expertInDb == null)
                return HttpNotFound();
            expertInDb.ActiveStatus = expert.ActiveStatus;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}