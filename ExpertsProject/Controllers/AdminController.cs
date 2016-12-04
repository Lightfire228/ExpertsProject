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
        public ActionResult AdminVerify()
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
        public ActionResult AdminActivate()
        {

            IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();

            users = from user in users
                    where !user.ActiveStatus
                    select user;

            return View(users);
        }
        public ActionResult Verify(ApplicationUser expert)
        {
            var expertInDb = _dbContext.Experts.Find(expert.Id);
            expertInDb.Validated = true;
            _dbContext.SaveChanges();
            return RedirectToAction("AdminVerify");
        }
        public ActionResult Deactivate(ApplicationUser expert)
        {
            var expertInDb = _dbContext.Users.Find(expert.Id);
            expertInDb.ActiveStatus = false;
            _dbContext.SaveChanges();
            return RedirectToAction("AdminDeactivate");
        }
        public ActionResult Activate(ApplicationUser expert)
        {
            var expertInDb = _dbContext.Users.Find(expert.Id);
            expertInDb.ActiveStatus = true;
            _dbContext.SaveChanges();
            return RedirectToAction("AdminActivate");
        }
    }
}